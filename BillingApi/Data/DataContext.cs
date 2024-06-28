using BillingApi.Data.Mappings;
using BillingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BillingApi.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        private IDbContextTransaction _transaction;

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<BillingLine> BillingLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Mappings
            
            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new BillingMapping());
            modelBuilder.ApplyConfiguration(new BillingLineMapping());

            #endregion
        }

        #region Transactions

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            if (Database.CurrentTransaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        #endregion
    }
}
