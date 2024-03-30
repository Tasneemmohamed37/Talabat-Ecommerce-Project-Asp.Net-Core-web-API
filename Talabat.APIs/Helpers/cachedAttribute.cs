using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services;

namespace Talabat.APIs.Helpers
{
    public class cachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSecond;

        public cachedAttribute(int timeToLiveInSecond) 
        {
            _timeToLiveInSecond = timeToLiveInSecond;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json"
                };

                context.Result = contentResult;
                return;
            }

            var executedEndPointContext = await next.Invoke(); // will execute the endPoint

            if(executedEndPointContext.Result is OkObjectResult okObjectResult)
            {
              await cacheService.CacheResponseAsync(cacheKey, okObjectResult, TimeSpan.FromSeconds(_timeToLiveInSecond));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            // {{baseUrl}}/api/products?pageIndex=1&pageSize=5&sort=name

            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path); // /api/products

            foreach (var (key,value) in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
                // /api/products|pageIndex-1|pageSize-5|sort-name
            }

            return keyBuilder.ToString();
        }
    }
}
