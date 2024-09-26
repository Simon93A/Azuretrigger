using FirstCompany.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstCompany.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToContainer("Customers")
                .HasPartitionKey(c => c.Id)
                .HasKey(c => c.Id);
        }


    }
}
