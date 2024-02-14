using Employee_Manager_Models;

namespace Employee_Manager_API.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        bool EmployeeExists(int empId);
        bool CreateEmployee(Employee emp);
        bool UpdateEmployee(Employee emp, int depID);
        bool DeleteEmployee(Employee emp);
        bool Save();
        Task<List<Employee>> Search(string searchTerm);
    }
}
