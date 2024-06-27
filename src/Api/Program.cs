using Api.Extensions;
using ExternalApiClient;
using ExternalApiClient.Settings;
using FluentValidation;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services;
using Services.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Billing Api",
            Contact = new OpenApiContact() { Name = "Tiago Cirelli", Email = "tiagocirelli@hotmail.com" },
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/license/mit/") }
        });
    })
    .AddDbContext<DataContext>(options =>
    {
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new Exception("ConnectionString \"DefaultConnection\" not found!");

        connectionString = Environment.ExpandEnvironmentVariables(connectionString);

        options
#if DEBUG
            .UseLoggerFactory(LoggerFactory.Create(p => p.AddConsole()))
            .EnableSensitiveDataLogging()
#endif
            .UseNpgsql(connectionString);
    })

    .AddAutoMapper(typeof(Domain.AutoMapperProfiles.CustomerProfile),
                   typeof(Services.AutoMapperProfiles.BillingProfile))

    .AddScoped<IRepositoryWrapper, RepositoryWrapper>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IBillingImportService, BillingImportService>()
    .AddScoped<IBillingService, BillingService>()

    .AddScoped<BillingClient>()

    .Configure<ApiClientSettings>(builder.Configuration.GetSection(nameof(ApiClientSettings)))

    .AddValidatorsFromAssemblyContaining<CustomerValidator>()

    .AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

ValidatorOptions.Global.LanguageManager = new CustomLanguageManager
{
    Culture = new System.Globalization.CultureInfo("en")
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
