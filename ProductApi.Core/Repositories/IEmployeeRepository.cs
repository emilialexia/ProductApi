using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Data;
using ProductApi.Core.Entities;


namespace ProductApi.Core.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<EmployeeEntity> GetAll();
        Task<EmployeeEntity?> GetByIdAsync(Guid id);
        Task<EmployeeEntity> AddAsync(EmployeeEntity employee);
        Task<EmployeeEntity> UpdateAsync(EmployeeEntity employee);
        Task<List<EmployeeEntity>> GetEmployeesByPositionAsync(string position);
        Task<bool> DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
