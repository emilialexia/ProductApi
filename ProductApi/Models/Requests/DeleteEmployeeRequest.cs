using System.Text.Json.Serialization;

namespace ProductApi.Models.Requests
{
    public class DeleteEmployeeRequest
    {
        public Guid Id { get; set; }
    }
}
