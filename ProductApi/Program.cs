using ProductApi.Core.Data;
using ProductApi.Core.Repositories;
using ProductApi.Core.Services;
using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Repositories;
using ProductApi.Core.Services;

namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost\\SQLEXPRESS;Database=ProductDb;Trusted_Connection=true;TrustServerCertificate=true;";

            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ProductApi"))
                    .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning))
            );

            // Register repository
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // Register service
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Apply migrations automatically (optional, for development)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}