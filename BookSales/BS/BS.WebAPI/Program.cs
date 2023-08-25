using BS.Business.Extensions;
using BS.WebAPI.Extensions;
using BS.WebAPI.Middlewares;


namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddAPIServices(builder.Configuration);

            builder.Services.AddBusinessServices();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseCustomException();


            app.Run();



        }
    }
}

