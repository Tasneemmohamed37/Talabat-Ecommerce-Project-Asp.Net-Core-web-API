
namespace Talabat.APIs.Errors
{
    //handel notFound & badRequest errors 
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode , string? message = null) 
        {
            StatusCode = statusCode ;

            Message = message ?? GetDefualtMessageForStatusCode(statusCode);// null coalescing operator
        }

        private string? GetDefualtMessageForStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                400 => "BadRequest" ,
                401 => "UnAuthorized",
                404 => "Resource not found",
                500 => "server error",
                _ => null // defualt
            };
        }
    }
}
