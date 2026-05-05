using ProductApi.Core.Entities;

namespace ProductApi.Core.Repositories
{
    public interface IProductRepository
    {
        IQueryable<ProductEntity> GetAll();
        Task<ProductEntity?> GetByIdAsync(Guid id);
        Task<ProductEntity> AddAsync(ProductEntity product);
        Task<ProductEntity> UpdateAsync(ProductEntity product);
        Task<bool> DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
