using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }


        // use first & last name with address in case change requets in the future and user have more than one address 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string AppUserId { get; set; } // Foreign Hey

        public AppUser User { get; set; } // nav prop one 
    }
}
