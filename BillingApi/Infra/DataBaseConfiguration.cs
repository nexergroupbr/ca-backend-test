using BillingApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BillingApi.Infra
{
    public static class DataBaseConfiguration
    {
        public static void RegisterDataBase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DataContext>(options => options
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(Configuration.GetConnectionString("SqlConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}
