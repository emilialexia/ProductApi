using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Core.Data.Configuration
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
            public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
            {
                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id)
                    .HasDefaultValueSql("NEWID()")
                    .IsRequired();

                builder.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsRequired();

                builder.Property(e => e.LastName)
                   .HasMaxLength(255)
                   .IsRequired();

                builder.Property(e => e.Position)
                   .HasMaxLength(255)
                   .IsRequired();

                builder.Property(e => e.Salary)
                    .IsRequired();

                builder.Property(e => e.CreatedAt)
                    .IsRequired();

                builder.Property(e => e.UpdatedAt)
                    .IsRequired();

            }
    }
}
