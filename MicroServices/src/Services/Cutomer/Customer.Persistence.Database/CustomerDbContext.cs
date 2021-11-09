using Customer.Domain;
using Customer.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Customer.Persistence.Database
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ModelConfig(modelBuilder);
        }

        public DbSet<Client> Clients { get; set; }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ClientConfiguration(modelBuilder.Entity<Client>());
        }

    }
}
