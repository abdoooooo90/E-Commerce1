using System.Net;
using Domain.Exceptions;
using Domain.Exceptions.product;
using Shared.ErrorModel;

namespace E_Commerce.MiddleWareas
{
    public class GlobalExeptionHandlingMiddlewar
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExeptionHandlingMiddlewar> _logger;

        public GlobalExeptionHandlingMiddlewar(RequestDelegate next , ILogger<GlobalExeptionHandlingMiddlewar> logger)
        {
             _next = next;
             _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext )
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode==(int) HttpStatusCode.NotFound)
                    await HandleNotFoundendPointException(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError($"something Went wrong {exception}");
                await HandleExceptionAsync(httpContext , exception);

            }
        }

        private async Task HandleNotFoundendPointException(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode= (int) HttpStatusCode .NotFound,
                ErrorMessage= $"The EndPoint{httpContext.Request.Path} NotFound"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        //HandleExceptions
        public async Task HandleExceptionAsync(HttpContext httpContext ,Exception exception)
        {
            //set Status Code 500 
            httpContext.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
            // Set Content Type "application / json"
            httpContext.Response.ContentType = "application/json";
            //C#09:
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
               _ => (int)HttpStatusCode.InternalServerError
            };
            //return Standered Response 
            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = exception.Message
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

    }
}
