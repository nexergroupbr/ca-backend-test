using BillingApi.Service.Interfaces;
using BillingApi.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BillingApi.Infra
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<DbContext>();
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            #region SERVIÇOS

            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            services.AddScoped(typeof(IProductService), typeof(ProductService));

            #endregion
        }
    }
}
