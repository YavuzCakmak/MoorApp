using Microsoft.AspNetCore.Diagnostics;
using Moor.Service.Exceptions;
using Moor.Service.Models.Dto.ResponseDto;
using System.Net;
using System.Text.Json;

namespace Moor.API.Middlewares
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => HttpStatusCode.BadRequest,
                        NotFoundException => HttpStatusCode.NotFound,
                        _ => HttpStatusCode.InternalServerError
                    };
                    context.Response.StatusCode = (int)statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail((int)statusCode, exceptionFeature.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
