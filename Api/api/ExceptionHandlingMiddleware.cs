using Newtonsoft.Json;
using System.Net;

namespace api
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new ApiErrorResponse()
                {
                    Message = "Bad request occurred.",
                    ExceptionMessage = ex.Message,
                    StackTrace = ex.StackTrace != null ? ex.StackTrace : ""
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
    public class ApiErrorResponse
    {
        public string Message { get; set; } = string.Empty;
        public string ExceptionMessage { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}
