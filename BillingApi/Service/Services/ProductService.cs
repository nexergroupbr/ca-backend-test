using BillingApi.Data;
using BillingApi.Domain.Models;
using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BillingApi.Service.Services
{
    public class ProductService(DataContext context,
                                IUtilsService utilsService) : IProductService
    {
        private readonly DataContext _context = context;
        private readonly IUtilsService _utilsService = utilsService;

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<Product?> GetProduct(Guid idProduct)
        {
            return await _context.Product.FindAsync(idProduct);
        }

        public async Task<Guid?> PostProduct(ProductViewModel product)
        {
            try
            {
                var productEntity = _utilsService.ConvertToEntity<Product, ProductViewModel>(product);

                _context.Add(productEntity);
                _context.SaveChanges();

                return productEntity.Id;
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao inserir o product: {ex.Message}");
            }

        }

        public async Task PutProduct(ProductViewModel product)
        {
            try
            {
                var productEntity = _utilsService.ConvertToEntity<Product, ProductViewModel>(product);

                _context.Entry(productEntity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao atualizar o product: {ex.Message}");
            }

        }

        public async Task DeleteProduct(Guid id)
        {
            try
            {
                var product = await GetProduct(id) ?? throw new KeyNotFoundException($"Registro {id} não encontrado.");
                _context.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao deletar o product: {ex.Message}");
            }

        }
    }
}
