using IdentityService.BusinessLogic.DTOs.Exception;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.DataAccess.Exceptions;

namespace IdentityService.API.Middlewares
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
            switch (exception)
            {
                case ArgumentException:
                    return (StatusCodes.Status400BadRequest, nameof(ArgumentException));

                case NotFoundException:
                    return (StatusCodes.Status404NotFound, nameof(NotFoundException));

                case DbOperationException:
                    return (StatusCodes.Status500InternalServerError, nameof(DbOperationException));

                default:
                    return (StatusCodes.Status500InternalServerError, nameof(Exception));
            }
        }
    }
}
