using Microsoft.EntityFrameworkCore;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region DbContext
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            #endregion

            #region Repos
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
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

            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); // insted of useRouting useEndPoint 
            #endregion

            app.Run();
        }
    }
}
