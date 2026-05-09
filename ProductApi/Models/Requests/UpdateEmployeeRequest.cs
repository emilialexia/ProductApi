using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.Requests
{
    public class UpdateEmployeeRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public double Salary { get; set; }
    }
}
