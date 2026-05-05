using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly List<Product> products = new List<Product>()
        {
            new Product("Lapte", 6.7, 35),
            new Product("Cafea", 12.3, 20)
        };

        
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return Ok(products); //new comment
        }

       
        [HttpPost]
        public ActionResult Add([FromBody] Product p)
        {
            if (p == null || string.IsNullOrWhiteSpace(p.Name) || p.Price <= 0 || p.Quantity <= 0)
                return BadRequest("Invalid product data");

            products.Add(p);
            return Ok("Product added successfully");
        }

        
        [HttpPost("purchase")]
        public ActionResult Purchase([FromBody] PurchaseRequest req)
        {
            if (req == null)
                return BadRequest("Invalid request");

            if (req.Quantity <= 0)
                return BadRequest("Invalid quantity");

            var product = products.FirstOrDefault(p => p.Name == req.Name);

            if (product == null)
                return NotFound("Product not found");

            if (req.Quantity > product.Quantity)
                return BadRequest("Not enough stock");

            product.Quantity -= req.Quantity;
            double total = req.Quantity * product.Price;

            return Ok($"Purchased {req.Name} for {total}. Remaining stock: {product.Quantity}");
        }
    }
}