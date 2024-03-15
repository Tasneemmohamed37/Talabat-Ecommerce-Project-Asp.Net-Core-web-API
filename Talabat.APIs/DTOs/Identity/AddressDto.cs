using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs.Identity
{
    public class AddressDto
    {

        // use first & last name with address in case change requets in the future and user have more than one address 

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
