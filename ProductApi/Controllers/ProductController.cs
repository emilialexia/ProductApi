using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.Requests;
using ProductApi.Models.Responses;
using ProductApi.Core.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<GetAllProductsResponse>> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            var response = new GetAllProductsResponse
            {
                Products = products.Select(p => new ProductResponseItem
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };
            return Ok(response);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductResponse>> GetById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound($"Product with id {id} not found");

            var response = new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            return Ok(response);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> Add([FromBody] AddProductRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0 || request.Quantity <= 0)
                return BadRequest("Invalid product data");

            var addedProduct = await _productService.AddProductAsync(request.Name, request.Price, request.Quantity);

            var response = new AddProductResponse
            {
                Id = addedProduct.Id,
                Name = addedProduct.Name,
                Price = addedProduct.Price,
                Quantity = addedProduct.Quantity
            };

            return CreatedAtAction(nameof(GetById), new { id = addedProduct.Id }, response);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateProductResponse>> Update(Guid id, [FromBody] UpdateProductRequest request)
        {
            if (request == null || request.Price <= 0 || request.Quantity <= 0)
                return BadRequest("Invalid product data");

            try
            {
                await _productService.UpdateProductAsync(id, request.Name, request.Price, request.Quantity);

                var response = new UpdateProductResponse
                {
                    Message = "Product updated successfully",
                    Success = true
                };

                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product with id {id} not found");
            }
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteProductResponse>> Delete(Guid id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
                return NotFound($"Product with id {id} not found");

            var response = new DeleteProductResponse
            {
                Message = "Product deleted successfully",
                Success = true
            };

            return Ok(response);
        }
    }
}
