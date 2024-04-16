using Employee_Manager_Models;
using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;

namespace Employee_Manager_Client.Services
{
    public interface IFrontendUserPanelService
    {
        //user status
        Task<bool> IsUserLoggedIn();

        //access service
        Task<ResponseModel> Login(LoginModel loginModel);
        Task<ResponseModel> Register(AdminInfo info);

        //department service
        Task<Department> CreateDepartment(Department dep);
        Task<List<Department>> GetAllDepartments();
        Task<Department> GetDepartment(int id);

        //employee service
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int empID);
        Task<ResponseModel> CreateEmp(Employee emp);
        Task<ResponseModel> UpdateEmp(Employee emp);
        Task<ResponseModel> DeleteEmp(int id);

        // address service
        Task<List<Address>> GetAddresses();
        Task<Address> GetAddressById(int addressId);

        //upload service
        Task<ResponseModel> ExportExcel(IFormFile file);
    }
}
