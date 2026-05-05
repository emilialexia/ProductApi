using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Data;
using ProductApi.Core.Entities;

namespace ProductApi.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<ProductEntity> GetAll()
        {
            return _dbContext.Products.AsQueryable();
        }

        public async Task<ProductEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProductEntity> AddAsync(ProductEntity product)
        {
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            var result = await _dbContext.Products.AddAsync(product);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity product)
        {
            product.UpdatedAt = DateTime.UtcNow;

            var result = _dbContext.Products.Update(product);
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return false;

            _dbContext.Products.Remove(product);
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
