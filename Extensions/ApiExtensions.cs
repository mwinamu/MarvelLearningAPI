using System.Text.Json;
using Marten;
using MarvelLearningAPI.Infrastructure;
using MarvelLearningAPI.Infrastructure.Interface;
using Weasel.Core;

namespace MarvelLearningAPI.Extensions;

public static class ApiExtensions
{
    public static WebApplicationBuilder BuilderServices(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower;
        });
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        webApplicationBuilder.Services.AddEndpointsApiExplorer();
        webApplicationBuilder.Services.AddSwaggerGen();
// This is the absolute, simplest way to integrate Marten into your
// .NET application with Marten's default configuration
        webApplicationBuilder.Services.AddScoped<IUserRepository, UserRepository>();

        var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        webApplicationBuilder.Services.AddMarten(options =>
        {
            // Establish the connection string to your Marten database
            options.Connection(connectionString);
            if (webApplicationBuilder.Environment.IsDevelopment())
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        });
        return webApplicationBuilder;
    }
    public static  WebApplication WebApplicationContainer(this WebApplication app)
    {
// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        return app;
    }
}