using ProductApi.Core.Entities;
using ProductApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductEntity>> GetAllProductsAsync()
        {
            return await _productRepository.GetAll().ToListAsync();
        }

        public async Task<ProductEntity?> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<ProductEntity> AddProductAsync(string name, double price, int quantity)
        {
            var productEntity = new ProductEntity
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var addedProduct = await _productRepository.AddAsync(productEntity);
            await _productRepository.SaveChangesAsync();

            return addedProduct;
        }

        public async Task<ProductEntity> UpdateProductAsync(Guid id, string name, double price, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with id {id} not found");

            product.Name = name;
            product.Price = price;
            product.Quantity = quantity;
            product.UpdatedAt = DateTime.UtcNow;

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var deleted = await _productRepository.DeleteAsync(id);

            if (!deleted)
                return false;

            await _productRepository.SaveChangesAsync();
            return true;
        }
    }
}
