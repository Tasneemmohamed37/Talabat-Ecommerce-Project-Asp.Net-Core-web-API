using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Custom_Middlewares;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.Core.Interfaces;
using Talabat.Reposatory.Data.Context;
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

            #region extention method include DI services

            builder.Services.ApplicationServices();

            #endregion

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
            app.UseStatusCodePagesWithReExecute("errors/{0}");
            #endregion

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers(); // insted of useRouting useEndPoint 
            #endregion

            app.Run();
        }
    }
}
