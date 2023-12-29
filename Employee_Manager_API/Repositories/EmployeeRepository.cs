using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDbClass dbClass;

        public EmployeeRepository(EmpDbClass dbClass)
        {
            this.dbClass = dbClass;
        }
        public async Task<Employee> AddEmployee(Employee emp)
        {
            emp.CreatedOn = DateTime.UtcNow.ToString();
            var result = await dbClass.Employee.AddAsync(emp);
            await dbClass.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int empId)
        {
            var result = await dbClass.Employee.FirstOrDefaultAsync(e => e.UserID == empId);
            if (result != null)
            {
                dbClass.Employee.Remove(result);
                await dbClass.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await dbClass.Employee.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await dbClass.Employee.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await dbClass.Employee.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task<Employee> UpdateEmployee(Employee emp)
        {
            var result = await dbClass.Employee.FirstOrDefaultAsync(e => e.UserID == emp.UserID);

            if (result != null)
            {
                result.UserID = emp.UserID;
                result.FirstName = emp.FirstName;
                result.LastName = emp.LastName;
                result.Email = emp.Email;
                result.DOB = emp.DOB;
                result.Gender = emp.Gender;

                await dbClass.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
