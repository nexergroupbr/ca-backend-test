using BillingApi.ViewModels;

namespace BillingApi.Service.Interfaces
{
    public interface IBillingLineService
    {
        Task<int?> PostBillingLine(BillingLineViewModel billingLine);
    }
}
