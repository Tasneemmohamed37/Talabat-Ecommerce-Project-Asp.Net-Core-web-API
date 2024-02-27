using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // swagger will ignore this controller when generate documentation
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int statusCode)
        {
            return NotFound(new ApiResponse(statusCode , "not found endPoint"));
        }
    }
}
