namespace ProductApi.Models.Responses
{
    public class GetAllEmployeesResponse
    {
        public List<EmployeeResponse> Employees { get; set; } = new();
    }

    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public double Salary { get; set; }
    }
}
