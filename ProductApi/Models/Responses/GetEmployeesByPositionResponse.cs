namespace ProductApi.Models.Responses
{
    public class GetEmployeesByPositionResponse
    {
        public List<EmployeePositionResponse> PositionsEmployees { get; set; } = new();
    }

    public class EmployeePositionResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public double Salary { get; set; }
    }
}
