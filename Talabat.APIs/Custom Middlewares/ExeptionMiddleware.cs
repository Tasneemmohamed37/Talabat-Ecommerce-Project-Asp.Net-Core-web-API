using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Custom_Middlewares
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;  // delegate for next middleware
        private readonly ILogger<ExeptionMiddleware> _logger; // log exeption details in console 'kestral'
        private readonly IHostEnvironment _env; // use it to check if env development will show exeption details , if production will not

        public ExeptionMiddleware(RequestDelegate next , ILogger<ExeptionMiddleware> logger , IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync (HttpContext context) // context for current request 
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex ,ex.Message); // ToDo -> log exeption in DB 'production mood'

                #region configure response msg header

                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                #endregion

                var response = _env.IsEnvironment("Development")?
                                new ApiExeptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                                : new ApiExeptionResponse((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // to make obj prop camelcase not pascal becouse js not understand pascal

                var jsonResponse = JsonSerializer.Serialize(response , options);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
