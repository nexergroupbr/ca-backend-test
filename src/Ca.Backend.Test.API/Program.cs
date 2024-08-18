using System.Reflection;
using Ca.Backend.Test.API.Middlewares;
using Ca.Backend.Test.Application.Mappings;
using Ca.Backend.Test.Infra.IoC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Adds controllers to the services container
    services.AddControllers();

    // Configures API behavior options
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // Suppresses the automatic model state validation
    });

    // Adds AutoMapper to the container with the specified profile
    services.AddAutoMapper(typeof(MappingProfile));

    // Adds services for API endpoints exploration and Swagger/OpenAPI configuration
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Customer Billing Management API .NET8 (Base)",
            Version = "v1",
            Description = "This project is a REST API developed in .NET 8.0 to manage customer billing.",
            Contact = new OpenApiContact
            {
                Name = "Kayque Almeida Piton",
                Email = "kayquepiton@gmail.com",
                Url = new Uri("https://github.com/kayquepiton")
            }
        });

        // Configures XML comments for Swagger
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

    // Configures application dependencies
    services.ConfigureAppDependencies(configuration);

    // Adds HttpClient to the container
    services.AddHttpClient();
}

void ConfigureMiddleware(WebApplication app)
{
    // Enables Swagger in development environment
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty; // Sets the Swagger UI at the app's root
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Billing Management API .NET8 (Base)");
        });
    }

    // Adds custom exception handling middleware
    app.UseMiddleware<ExceptionMiddleware>();

    // Adds routing middleware
    app.UseRouting();

    // Redirects HTTP requests to HTTPS
    app.UseHttpsRedirection();

    // Maps attribute-routed controllers
    app.MapControllers();
}
