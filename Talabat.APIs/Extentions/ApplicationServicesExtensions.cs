﻿using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;
using Talabat.Reposatory.Repositories;
using Talabat.Services;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            #region Repos
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            #endregion

            #region Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            #endregion

            #region Auto Mapper

            // work transiant becouse for sure in one request need to map only one time 
            services.AddAutoMapper(typeof(mappingProfiles));

            #endregion

            #region Configure apiBehavior [validation error]
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            #endregion

            return services;
        }
    }
}
