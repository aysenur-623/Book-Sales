using BS.Business.CustomExceptions;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace BS.WebAPI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            //UseExceptionHandler --> .net içindeki zaten exceptionları yakalayabilen bir middleware
            app.UseExceptionHandler(config =>
            {
                // Run metodu response u döndürmek için kullanılır
                config.Run(async context =>
                {
                    // Aşağıdaki 2 satırla yakalanan Exception tipindeki nesneyi elimize alıyoruz
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionFeature.Error;

                    var statusCode = StatusCodes.Status500InternalServerError;

                    switch (exception)
                    {
                        case BadRequestException:
                            statusCode = StatusCodes.Status400BadRequest;
                            break;
                        case NotFoundException:
                            statusCode = StatusCodes.Status404NotFound;
                            break;
                        default:
                            break;
                    }

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    var response = ApiResponse<NoData>.Fail(statusCode, exception.Message);

                    // ELDE EDİLEN EXCEPTİON I LOGLA

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });
        }
    }
}
