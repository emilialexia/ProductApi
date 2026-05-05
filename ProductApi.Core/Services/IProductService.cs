using ProductApi.Core.Entities;

namespace ProductApi.Core.Services
{
    public interface IProductService
    {
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity?> GetProductByIdAsync(Guid id);
        Task<ProductEntity> AddProductAsync(string name, double price, int quantity);
        Task<ProductEntity> UpdateProductAsync(Guid id, string name, double price, int quantity);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
