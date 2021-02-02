using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Shared.ApiModels.Api;
using PhotoGallery.Shared.Exceptions;

namespace PhotoGalery.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadRequestException badRequestException)
            {
                await WriteContext(httpContext, badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                await WriteContext(httpContext, notFoundException.Message);
            }
            catch (Exception exception)
            {
                await WriteContext(httpContext, exception.Message);
            }
        }

        private async Task WriteContext(HttpContext httpContext, string errorMessage)
        {
            httpContext.Response.StatusCode = 400;

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(new ErrorResponse {ErrorMessage = errorMessage}));
        }
    }
}