using BillingApi.Domain.Models;
using BillingApi.ViewModels;

namespace BillingApi.Service.Interfaces
{
    public interface IBillingService
    {
        Task<IEnumerable<Billing>> GetBillings();
        Task<Billing?> GetBilling(int id);
        Task<int?> PostBilling(BillingViewModel billing);
        void PutBilling(BillingViewModel billing);
        Task DeleteBilling(int id);
        Task GetApiBillingsFirstCustomerFirstProduct();
        Task ProcessApiBilling();
    }
}
