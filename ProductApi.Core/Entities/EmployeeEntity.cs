using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Core.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public double Salary { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
