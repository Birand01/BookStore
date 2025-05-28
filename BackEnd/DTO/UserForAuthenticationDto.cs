using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}