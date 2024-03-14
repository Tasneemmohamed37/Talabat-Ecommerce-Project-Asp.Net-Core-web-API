using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.Identity
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }

        
        [EmailAddress]
        public string Email { get; set; }

        
        public string PhoneNumber { get; set; }

        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
           ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}
