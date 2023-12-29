using Employee_Manager_Models;

namespace Employee_Manager_API.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> GetDepartment(int depID);
        Task<Department> AddDepartment(Department dep);
        Task<Department> UpdateDepartment(Department updateDep);
        Task<Department> DeleteDepartment(Department depID);
    }
}
