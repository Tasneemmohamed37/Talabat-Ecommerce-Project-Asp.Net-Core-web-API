using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Reposatory.Identity.context;
using Talabat.Services;

namespace Talabat.APIs.Extentions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection IdentityServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            return services;

        }
    }
}
