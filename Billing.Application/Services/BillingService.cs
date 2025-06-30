using Billing.Application.DTOs;
using Billing.Domain.Interfaces;

namespace Billing.Application.Services
{
    public class BillingService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBillingRepository _billingRepository;

        public BillingService(ICustomerRepository customerRepo, IProductRepository productRepo, IBillingRepository billingRepo)
        {
            _customerRepository = customerRepo;
            _productRepository = productRepo;
            _billingRepository = billingRepo;
        }

        public async Task ProcessBillingAsync(BillingsDto billingDto)
        {
            var customer = await _customerRepository.GetByIdAsync(billingDto.CustomerId);
            if (customer == null) throw new Exception("Cliente não encontrado");

            foreach (var line in billingDto.Lines)
            {
                var product = await _productRepository.GetByIdAsync(line.ProductId);
                if (product == null) throw new Exception($"Produto {line.ProductId} não encontrado");
            }
            // Persist billing
        }
    }
}
