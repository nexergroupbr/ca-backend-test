using BillingApi.Data;
using BillingApi.Domain.Models;
using BillingApi.Service.Interfaces;
using BillingApi.ViewModels;

namespace BillingApi.Service.Services
{
    public class BillingLineService(DataContext context,
                              IUtilsService utilsService) : IBillingLineService
    {
        private readonly DataContext _context = context;
        private readonly IUtilsService _utilsService = utilsService;

        public async Task<int?> PostBillingLine(BillingLineViewModel billingLine)
        {
            try
            {
                if (billingLine.Id is not null)
                    billingLine.Id = null;

                var billingLineEntity = _utilsService.ConvertToEntity<BillingLine, BillingLineViewModel>(billingLine);

                _context.Add(billingLineEntity);
                _context.SaveChanges();

                return billingLineEntity.Id;
            }
            catch (Exception ex)
            {
                _context.Rollback();
                throw new Exception($"Erro ao inserir o billing line: {ex.Message}");
            }
        }
    }
}
