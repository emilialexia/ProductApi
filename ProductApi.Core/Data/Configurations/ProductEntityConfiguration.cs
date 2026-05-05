using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Core.Entities;

namespace ProductApi.Core.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(e => e.Price)
                .IsRequired();

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.UpdatedAt)
                .IsRequired();

            builder.HasData(
                new ProductEntity 
                { 
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"), 
                    Name = "Lapte", 
                    Price = 6.7, 
                    Quantity = 35, 
                    CreatedAt = new DateTime(2026, 5, 5, 17, 0, 0, DateTimeKind.Utc), 
                    UpdatedAt = new DateTime(2026, 5, 5, 17, 0, 0, DateTimeKind.Utc) 
                },
                new ProductEntity 
                { 
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"), 
                    Name = "Cafea", 
                    Price = 12.3, 
                    Quantity = 20, 
                    CreatedAt = new DateTime(2026, 5, 5, 17, 0, 0, DateTimeKind.Utc), 
                    UpdatedAt = new DateTime(2026, 5, 5, 17, 0, 0, DateTimeKind.Utc) 
                }
            );
        }
    }
}
