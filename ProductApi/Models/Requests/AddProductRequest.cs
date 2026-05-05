namespace ProductApi.Models.Requests
{
    public class AddProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
