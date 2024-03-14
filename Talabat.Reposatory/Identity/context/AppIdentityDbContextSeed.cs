using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Reposatory.Identity.context
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "tasneem mohamed",
                    Email = "tasnemmuhammed37@gmail.com",
                    UserName = "tasneem.mohamed",
                    PhoneNumber = "01028249977"
                };

                await userManager.CreateAsync(user , "P@$$w0rd");
            }
        }
    }
}
