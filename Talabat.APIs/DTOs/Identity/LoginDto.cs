using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.Identity
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }

        
        public string Password { get; set; }
    }
}
