using Microsoft.EntityFrameworkCore;
using ProductApi.Core.Data;
using ProductApi.Core.Entities;


namespace ProductApi.Core.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ProductDbContext _dbContext;

        public EmployeeRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<EmployeeEntity> GetAll()
        {
            return _dbContext.Employees.AsQueryable();
        }

        public async Task<EmployeeEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EmployeeEntity> AddAsync(EmployeeEntity employee)
        {
            employee.Id = Guid.NewGuid();
            employee.CreatedAt = DateTime.UtcNow;
            employee.UpdatedAt = DateTime.UtcNow;

            var result = await _dbContext.Employees.AddAsync(employee);

            return result.Entity;
        }

        public Task<EmployeeEntity> UpdateAsync(EmployeeEntity employee)
        {
            employee.UpdatedAt = DateTime.UtcNow;

            var result = _dbContext.Employees.Update(employee);

            return Task.FromResult(result.Entity);
        }

        public async Task<List<EmployeeEntity>> GetEmployeesByPositionAsync(string position)
        {
            return await _dbContext.Employees
                .Where(e => e.Position == position)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                return false;
            _dbContext.Employees.Remove(employee);
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
