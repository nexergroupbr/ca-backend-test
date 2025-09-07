using Domain.Models;
using Repository.Context;
using Repository.Interfaces;

namespace Repository.RepositoryClass
{
    public class Repository : IRepository<Billing>
    {
        BillingDbContext _context;

        public Repository(BillingDbContext context)
        {
            _context = context;
        }            

    }
}
