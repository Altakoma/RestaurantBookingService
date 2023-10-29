using OrderService.Application.DTOs.Exception;
using OrderService.Domain.Exceptions;

namespace OrderService.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception generalException)
            {
                (int statusCode, string exceptionType) =
                    GetStatusCodeAndExceptionTypeName(generalException);

                context.Response.StatusCode = statusCode;

                var exceptionDTO = new ExceptionDTO
                {
                    Message = generalException.Message,
                    ExceptionType = exceptionType,
                    Data = generalException.InnerException,
                };

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(exceptionDTO);
            }
        }

        private (int, string) GetStatusCodeAndExceptionTypeName(Exception exception)
        {
            return exception switch
            {
                ArgumentException => (StatusCodes.Status400BadRequest, nameof(ArgumentException)),
                NotFoundException => (StatusCodes.Status404NotFound, nameof(NotFoundException)),
                DbOperationException => (StatusCodes.Status500InternalServerError, nameof(DbOperationException)),
                _ => (StatusCodes.Status500InternalServerError, nameof(Exception)),
            };
        }
    }
}
