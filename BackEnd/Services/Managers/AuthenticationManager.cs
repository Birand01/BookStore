using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BackEnd.DTO;
using BackEnd.Models;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Identity;
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

        public async Task<string> CreateToken()
        {
            var signingCredentials=GetSigningCredentials();
            var claims=await GetClaims();
            var tokenOptions=GenerateTokenOptions(signingCredentials,claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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
    }
}