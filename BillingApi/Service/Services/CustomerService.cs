using BillingApi.Data;
using BillingApi.Domain.Models;
using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BillingApi.Service.Services
{
    public class CustomerService(DataContext context,
                           IUtilsService utilsService) : ICustomerService
    {
        private readonly DataContext _context = context;
        private readonly IUtilsService _utilsService = utilsService;

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer?> GetCustomer(Guid idCustomer)
        {
            return await _context.Customer.FindAsync(idCustomer);
        }

        public async Task<Guid?> PostCustomer(CustomerViewModel customer)
        {
            try
            {
                var customerEntity = _utilsService.ConvertToEntity<Customer, CustomerViewModel>(customer);

                _context.Add(customerEntity);
                _context.SaveChanges();

                return customerEntity.Id;
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao inserir o customer: {ex.Message}");
            } 
        }

        public async Task PutCustomer(CustomerViewModel customer)
        {
            try
            {
                var customerEntity = _utilsService.ConvertToEntity<Customer, CustomerViewModel>(customer);

                _context.Entry(customerEntity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao atualizar o customer: {ex.Message}");
            }
        }

        public async Task DeleteCustomer(Guid id)
        {
            try
            {
                var customer = await GetCustomer(id) ?? throw new KeyNotFoundException($"Registro {id} não encontrado.");
                _context.Remove(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao deletar o customer: {ex.Message}");
            }
        }
    }
}
