using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Core.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeEntity>> GetAllEmployeesAsync();

        Task<EmployeeEntity?> GetEmployeeByIdAsync(Guid id);

        Task<EmployeeEntity> AddEmployeeAsync(string firstName, string lastName, string position, double salary);

        Task<EmployeeEntity> UpdateEmployeeAsync(Guid id, string firstName,
            string lastName,
            string position,
            double salary);

        Task<List<EmployeeEntity>> GetEmployeesByPositionAsync(string position);

        Task<EmployeeEntity?> HighestSalaryEmployeeAsync();
 
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
