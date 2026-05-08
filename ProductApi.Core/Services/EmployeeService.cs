using ProductApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeEntity>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAll().ToListAsync();
        }

        public async Task<EmployeeEntity?> GetEmployeeByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<EmployeeEntity> AddEmployeeAsync(string firstName, string lastName, string position, double salary)
        {
            var employeeEntity = new EmployeeEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Position = position,
                Salary = salary,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var addedEmployee = await _employeeRepository.AddAsync(employeeEntity);

            await _employeeRepository.SaveChangesAsync();

            return addedEmployee;
        }

        public async Task<List<EmployeeEntity>> GetEmployeesByPositionAsync(string position)
        {
            return await _employeeRepository.GetEmployeesByPositionAsync(position);
        }

        public async Task<EmployeeEntity> UpdateEmployeeAsync(Guid id, string firstName, string lastName, string position, double salary)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new KeyNotFoundException($"Employee with id {id} not found");
            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Position = position;
            employee.Salary = salary;
            employee.UpdatedAt = DateTime.UtcNow;
            await _employeeRepository.UpdateAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return employee;
        }


        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var deleted = await _employeeRepository.DeleteAsync(id);

            if (!deleted)
                return false;

            await _employeeRepository.SaveChangesAsync();

            return true;
        }
    }
}
