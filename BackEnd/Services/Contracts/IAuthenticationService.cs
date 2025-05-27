using BackEnd.DTO;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserRegistrationDto userRegistrationDto);
        
    }
}