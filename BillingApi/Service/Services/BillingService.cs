using BillingApi.Data;
using BillingApi.Domain.Models;
using BillingApi.Service.Dto;
using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using BillingApi.Infra.Extensions;

namespace BillingApi.Service.Services
{
    public class BillingService(DataContext context,
                          IUtilsService utilsService,
                          IConfiguration configuration,
                          IApiService apiService,
                          ICustomerService customerService,
                          IProductService productService,
                          IBillingLineService billingLineService) : IBillingService
    {
        private readonly DataContext _context = context;
        private readonly IUtilsService _utilsService = utilsService;
        private readonly IConfiguration _configuration = configuration;
        private readonly IApiService _apiService = apiService;
        private readonly ICustomerService _customerService = customerService;
        private readonly IProductService _productService = productService;
        private readonly IBillingLineService _billingLineService = billingLineService;

        public async Task<IEnumerable<Billing>> GetBillings()
        {
            return await _context.Billing
                                 .Include(b => b.Customer)
                                 .Include(b => b.BillingLines)
                                 .ThenInclude(bl => bl.Product)
                                 .ToListAsync();
        }

        public async Task<Billing?> GetBilling(int id)
        {
            return await _context.Billing
                                 .Include(b => b.Customer)
                                 .Include(b => b.BillingLines)
                                 .ThenInclude(bl => bl.Product)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<int?> PostBilling(BillingViewModel billing)
        {
            try
            {
                if (billing.Id is not null)
                    billing.Id = null;

                var billingEntity = _utilsService.ConvertToEntity<Billing, BillingViewModel>(billing);

                _context.Add(billingEntity);
                _context.SaveChanges();

                return billingEntity.Id;
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao inserir o billing: {ex.Message}");
            }
        }

        public void PutBilling(BillingViewModel billing)
        {
            try
            {
                var billingEntity = _utilsService.ConvertToEntity<Billing, BillingViewModel>(billing);

                _context.Entry(billingEntity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao atualizar o billing: {ex.Message}");
            }
        }

        public async Task DeleteBilling(int id)
        {
            try
            {
                var billing = await GetBilling(id) ?? throw new KeyNotFoundException($"Registro {id} não encontrado.");
                _context.Remove(billing);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao deletar o billing: {ex.Message}");
            }
        }

        public async Task GetApiBillingsFirstCustomerFirstProduct()
        {
            try
            {
                string url = _configuration.GetValue<string>("Integracoes:Billing:GetBillings:Url");

                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException("Url não encontrada.");

                var jsonApiBillings = _apiService.GetData(url);
                var rootObjectBillingList = jsonApiBillings.DeserializeObject<List<RootObjectBilling>>();

                var primeiroRegistro = rootObjectBillingList.FirstOrDefault();

                if (primeiroRegistro is not null)
                {
                    if (await _customerService.GetCustomer(primeiroRegistro.Customer.Id) == null)
                    {
                        var customer = new CustomerViewModel()
                        {
                            Id = primeiroRegistro.Customer.Id,
                            Name = primeiroRegistro.Customer.Name,
                            Email = primeiroRegistro.Customer.Email,
                            Address = primeiroRegistro.Customer.Address
                        };

                        await _customerService.PostCustomer(customer);
                    }

                    foreach (var item in primeiroRegistro.BillingLines)
                    {
                        if (await _productService.GetProduct(item.ProductId) == null)
                        {
                            var product = new ProductViewModel()
                            {
                                Id = item.ProductId,
                                Name = item.Description
                            };

                            await _productService.PostProduct(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um problema ao processar os primeiros dados do billing: {ex.Message}");
            }
        }

        public async Task ProcessApiBilling()
        {
            try
            {
                string url = _configuration.GetValue<string>("Integracoes:Billing:GetBillings:Url");

                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException("Url não encontrada.");

                var jsonApiBillings = _apiService.GetData(url);
                var rootObjectBillingList = jsonApiBillings.DeserializeObject<List<RootObjectBilling>>();

                var customerExists = true;
                var productExists = true;
                foreach (var item in rootObjectBillingList)
                {
                    var billing = new BillingViewModel()
                    {
                        BillingLines = item.BillingLines,
                        Currency = item.Currency,
                        CustomerId = item.Customer.Id,
                        Date = item.Date,
                        DueDate = item.DueDate,
                        InvoiceNumber = item.InvoiceNumber,
                        TotalAmount = item.TotalAmount
                    };

                    if (await _customerService.GetCustomer(item.Customer.Id) == null)
                        customerExists = false;

                    var listBillingLines = new List<BillingLineViewModel>();
                    foreach (var itemBillingLine in item.BillingLines)
                    {
                        if (await _productService.GetProduct(itemBillingLine.ProductId) == null)
                        {
                            productExists = false;
                            break;
                        }

                        var billingLine = new BillingLineViewModel()
                        {
                            BillingId = 0,
                            Description = itemBillingLine.Description,
                            ProductId = itemBillingLine.ProductId,
                            Quantity = itemBillingLine.Quantity,
                            SubTotal = itemBillingLine.SubTotal,
                            UnitPrice = itemBillingLine.UnitPrice
                        };
                        listBillingLines.Add(billingLine);
                    }

                    if (!customerExists || !productExists)
                        throw new KeyNotFoundException("É necessário criar o registro faltante.");

                    foreach (var registro in listBillingLines)
                    {
                        var billingId = await PostBilling(billing);

                        if (billingId is not null)
                            registro.BillingId = (int)billingId;

                        await _billingLineService.PostBillingLine(registro);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um problema ao processar os dados do billing: {ex.Message}");
            }
        }
    }
}
