using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Application.Abstraction.Models.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
