using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;

namespace Employee_Manager_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return _context.Tbl_Employee.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _context.Tbl_Employee.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool EmployeeExists(int id)
        {
            return _context.Tbl_Employee.Any(e => e.Id == id);
        }

        public bool CreateEmployee(Employee emp)
        {
            emp.CreatedOn = DateTime.UtcNow;
            _context.Add(emp);
            return Save();
        }

        public bool UpdateEmployee(Employee emp)
        {
            _context.Update(emp);
            return Save();
        }

        public bool DeleteEmployee(Employee emp)
        {
            _context.Remove(emp);
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
