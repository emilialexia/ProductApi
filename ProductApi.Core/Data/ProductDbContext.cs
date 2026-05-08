using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Data.Configuration;
using ProductApi.Core.Data.Configurations;
using ProductApi.Core.Entities;

namespace ProductApi.Core.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        }
    }
}
