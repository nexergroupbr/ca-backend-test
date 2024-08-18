using System.Diagnostics.CodeAnalysis;
using Ca.Backend.Test.Application.Models.Request;
using Ca.Backend.Test.Application.Services;
using Ca.Backend.Test.Application.Services.Interfaces;
using Ca.Backend.Test.Application.Validators;
using Ca.Backend.Test.Domain.Entities;
using Ca.Backend.Test.Infra.Data;
using Ca.Backend.Test.Infra.Data.Repository;
using Ca.Backend.Test.Infra.Data.Repository.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ca.Backend.Test.Infra.IoC;
[ExcludeFromCodeCoverage]
public static class IoCServiceExtension
{
    public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDbContext(services, configuration);
        services.AddScoped<IGenericRepository<CustomerEntity>, GenericRepository<CustomerEntity>>();
        services.AddScoped<IGenericRepository<CustomerEntity>, GenericRepository<CustomerEntity>>();
        services.AddScoped<IGenericRepository<ProductEntity>, GenericRepository<ProductEntity>>();
        services.AddScoped<IGenericRepository<BillingEntity>, BillingRepository>();
        services.AddScoped<IGenericRepository<BillingLineEntity>, GenericRepository<BillingLineEntity>>();
        
        services.AddScoped<ICustomerServices, CustomerServices>();
        services.AddScoped<IProductServices, ProductServices>();
        services.AddScoped<IBillingServices, BillingServices>();

        services.AddScoped<IValidator<ProductRequest>, ProductRequestValidator>();
        services.AddScoped<IValidator<CustomerRequest>, CustomerRequestValidator>();
        services.AddScoped<IValidator<BillingRequest>, BillingRequestValidator>();

    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
    }
}
