using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BackEnd.DTO;
using BackEnd.Extensions;
using BackEnd.Models;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Services.Managers
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private readonly IConfiguration _configuration;
        private User? _user;
      

        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials=GetSigningCredentials();
            var claims=await GetClaims();
            var tokenOptions=GenerateTokenOptions(signingCredentials,claims);
            if(populateExp)
            {
                var refreshToken=GenerateRefreshToken();
                _user.RefreshToken=refreshToken;
                _user.RefreshTokenExpiryTime=DateTime.Now.AddDays(7);
                await _userManager.UpdateAsync(_user);
            }


            var accessToken=new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto{AccessToken=accessToken,RefreshToken=_user.RefreshToken};
        }
 

        public async Task<IdentityResult> RegisterUser(UserRegistrationDto userRegistrationDto)
        {

            var user=_mapper.Map<User>(userRegistrationDto);

            var result=await _userManager.CreateAsync(user,userRegistrationDto.Password);

           if(result.Succeeded)
           {
            await _userManager.AddToRolesAsync(user,userRegistrationDto.Roles);
           }

           return result;
            
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthenticationDto)
        {
            _user=await _userManager.FindByNameAsync(userForAuthenticationDto.UserName);
            var result=(_user!=null) && await _userManager.CheckPasswordAsync(_user,userForAuthenticationDto.Password);
            if(!result)
            {
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
            }
            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings=_configuration.GetSection("JwtSettings");
            var secretKey=jwtSettings["secretKey"];
            var secretKeyBytes=Encoding.UTF8.GetBytes(secretKey);
            var signingKey=new SymmetricSecurityKey(secretKeyBytes);
            return new SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.Name,_user.UserName)
            };
            var roles=await _userManager.GetRolesAsync(_user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }
            return claims;
        }

 
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim> claims)
        {
            var jwtSettings=_configuration.GetSection("JwtSettings");
            var tokenOptions=new JwtSecurityToken(
                issuer:jwtSettings["validIssuer"],
                audience:jwtSettings["validAudience"],
                claims:claims,
                expires:DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials:signingCredentials
            );
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber=new byte[32];
            using(var rng=RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                  return Convert.ToBase64String(randomNumber);
            }
          
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings=_configuration.GetSection("JwtSettings");
            var secretKey=jwtSettings["secretKey"];
            var tokenValidationParameters=new TokenValidationParameters
            {
                ValidateAudience=true,
                ValidateIssuer=true,
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:secretKey").Value)),
                ValidateLifetime=false
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var principal=tokenHandler.ValidateToken(token,tokenValidationParameters,out var securityToken);
            var jwtSecurityToken=securityToken as JwtSecurityToken;
            if(jwtSecurityToken==null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal=GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user=_userManager.Users.SingleOrDefault(u=>u.UserName==principal.Identity.Name);
            if(user==null || user.RefreshToken!=tokenDto.RefreshToken || user.RefreshTokenExpiryTime<=DateTime.Now)
            {
                throw new RefreshTokenBadRequest();
            }
            _user=user;
            return await CreateToken(populateExp:false); 
        }

    }
}