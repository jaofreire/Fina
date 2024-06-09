using Microsoft.EntityFrameworkCore;
using Fina.Core.Models;
using Fina.Api.Data.Map;


namespace Fina.Api.Data
{
    public class FinaDbContext : DbContext
    {
        public FinaDbContext(DbContextOptions<FinaDbContext> options) : base(options)
        { 
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
