using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.Requests
{
    public class UpdateProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
