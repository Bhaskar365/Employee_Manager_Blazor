using Employee_Manager_Models;

namespace Employee_Manager_API.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(Employee emp);
        Task<Employee> UpdateEmployee(Employee emp);
        Task<Employee> DeleteEmployee(int empId);
    }
}
