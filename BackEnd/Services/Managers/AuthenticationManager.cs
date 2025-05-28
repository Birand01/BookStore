using AutoMapper;
using BackEnd.DTO;
using BackEnd.Models;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Identity;

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
            return result;
        }
    }
}