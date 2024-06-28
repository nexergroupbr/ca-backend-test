using BillingApi.Domain.Models;
using BillingApi.ViewModels;

namespace BillingApi.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProduct(Guid idProduct);
        Task<Guid?> PostProduct(ProductViewModel product);
        Task PutProduct(ProductViewModel product);
        Task DeleteProduct(Guid id);
    }
}
