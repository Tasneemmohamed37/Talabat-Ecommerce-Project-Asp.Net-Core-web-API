using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Custom_Middlewares;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Interfaces;
using Talabat.Reposatory.Data.Context;
using Talabat.Reposatory.Identity.context;
using Talabat.Reposatory.Repositories;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             
            #region Add services to the container [ Allow DI ]

            builder.Services.AddControllers();

            #region extention method include swagger services
            builder.Services.AddSwaggerServices();
            #endregion

            #region DbContext
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            #endregion

            #region IdentityDbcontext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            #endregion

            #region Redis

            // use Singleton to use same obj while user use wabsite
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });

            #endregion

            #region extention method include DI services

            builder.Services.ApplicationServices();

            #region Identity 

            builder.Services.IdentityServices(builder.Configuration);
            #endregion

            #endregion


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPloicy", policy =>
                {
                    policy.WithOrigins(builder.Configuration["AngularBaseUrl"])
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .SetIsOriginAllowed(origin => true);
                });
            });
            #endregion


            var app = builder.Build();

            #region Update Database

            //every time run project apply all migrations which wasn`t apply

            var scope = app.Services.CreateScope(); // container for scoped services 'all services work scoped' 
            
            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // used to log exeption in kestral

            try
            {
                StoreContext DbContext = services.GetRequiredService<StoreContext>(); //Ask Explicitly

                await DbContext.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(DbContext);

                AppIdentityDbContext IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>(); //Ask Explicitly

                await IdentityDbContext.Database.MigrateAsync();


                var userManeger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManeger);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger(typeof(Program));
                logger.LogError(ex, "an Error occured during apply the migration");
            }



            #endregion

            #region Configure the HTTP request pipeline [ kestral middelwares]

            #region custom middleware [ exeption middleware ]

            app.UseMiddleware<ExeptionMiddleware>();

            #endregion

            if (app.Environment.IsDevelopment())
            {
                #region extention method include swagger middlewares
                app.UseSwaggerMiddlewares();
                #endregion
            }

            #region handle notFound endPoint
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            #endregion

            app.UseHttpsRedirection();

            app.UseCors("MyPloicy");

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers(); // insted of useRouting useEndPoint 
            #endregion

            app.Run();
        }
    }
}
