using BillingApi.Configuration;
using BillingApi.Data;
using BillingApi.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureServices(builder.Configuration);

// Jwt Configuration
builder.Services.ConfigureJwt(builder.Configuration);

// Swagger Configuration
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
var env = app.Environment;
var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

app.ConfigurePipeline(env, dataContext);

app.Run();
