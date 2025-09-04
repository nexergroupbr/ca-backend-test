using Domain.Models;
using Repository.Context;

namespace Services
{
    public class BillingServices
    {
        private readonly BillingDbContext _context;

        public BillingServices(BillingDbContext context)
        {
            _context = context;
        }

        public async Task<Billing> CreateBilling(int customerId, int productId, int quantity, decimal UnitPrice)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            var product = await _context.Products.FindAsync(productId);

            if (customer == null && product == null)
                throw new Exception("Customer or product not found.");

            if (product == null)
                throw new Exception("Product not found.");

            if (customer == null)
                throw new Exception("Customer not found.");

            var billing = new Billing
            {
                CustomerId = customerId,
                Date = DateOnly.MaxValue,
            };

            var billingLine = new BillingLine
            {
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = UnitPrice,
            };

            billing.BillingLines = new List<BillingLine> { billingLine };

            await _context.Billings.AddAsync(billing);
            await _context.SaveChangesAsync();

            return billing;
        }


    }
}

