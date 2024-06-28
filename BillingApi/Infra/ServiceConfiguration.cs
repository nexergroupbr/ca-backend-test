using BillingApi.Service.Interfaces;
using BillingApi.Service.Services;
using Microsoft.EntityFrameworkCore;

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

            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IBillingLineService, BillingLineService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUtilsService, UtilsService>();

            #endregion
        }
    }
}
