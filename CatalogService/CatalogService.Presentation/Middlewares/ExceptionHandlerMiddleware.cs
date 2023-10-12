using CatalogService.Application.DTOs.Exception;
using CatalogService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception generalException)
            {
                string exceptionType;
                object? data = null;

                switch (generalException)
                {
                    case ArgumentException concreteException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        exceptionType = typeof(ArgumentException).ToString();
                        data = concreteException.InnerException;
                        break;

                    case NotFoundException:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        exceptionType = typeof(NotFoundException).ToString();
                        break;

                    case DbOperationException:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        exceptionType = typeof(DbOperationException).ToString();
                        break;

                    case DbUpdateConcurrencyException:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        exceptionType = typeof(DbUpdateConcurrencyException).ToString();
                        break;

                    default:
                        exceptionType = typeof(Exception).ToString();
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        data = generalException.InnerException;
                        break;
                }

                var exceptionDTO = new ExceptionDTO
                {
                    Message = generalException.Message,
                    ExceptionType = exceptionType,
                    Data = data,
                };

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(exceptionDTO);
            }
        }
    }
}
