using Employee_Manager_Models;
using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Employee_Manager_Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        //LOGIN REGISTER SERVICE
        public async Task<ResponseModel> Login(LoginModel loginModel)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<LoginModel>("api/admin/login", loginModel);
            ResponseModel result = await response.Content.ReadFromJsonAsync<ResponseModel>();

            return result;
        }

        public async Task<ResponseModel> Register(AdminInfo info)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<AdminInfo>("api/admin/Create", info);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                ResponseModel result = JsonConvert.DeserializeObject<ResponseModel>(content);
                return result;
            }
            return null;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // DEPARTMENT SERVICE
        public async Task<Department> CreateDepartment(Department dep)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/department", dep);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Department createdEmp = JsonConvert.DeserializeObject<Department>(content);
                return createdEmp;
            }
            return null;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            return await _httpClient.GetFromJsonAsync<List<Department>>("api/department/GetAllDepartments");
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>($"api/department/{id}");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // EMPLOYEE SERVICE
        public async Task<List<Employee>> GetEmployees()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee/GetAllEmployees");
            return result;
        }

        public async Task<Employee> GetEmployeeById(int empID)
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"api/employee/{empID}");
        }

        public async Task<ResponseModel> CreateEmp(Employee emp)
        {
            try
            {
                Department department = await GetDepartment(emp.DepartmentId);

                if (department != null)
                {
                    emp.Department = department;

                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", emp);

                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return new ResponseModel { Status = true, Message = "Employee created successfully" };
                    }
                    else
                    {
                        // Handle other status codes
                        return new ResponseModel { Status = false, Message = $"Failed to create employee. Status code: {response.StatusCode}" };
                    }
                }
                else
                {
                    return new ResponseModel { Status = false, Message = "Department not found" };
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new ResponseModel { Status = false, Message = "An error occurred while processing your request" };
            }
        }

        public async Task<ResponseModel> UpdateEmp(Employee emp) 
        {
            try 
            {
                if(emp!= null) 
                {
                    HttpResponseMessage res = await _httpClient.PutAsJsonAsync($"api/employee", emp.EmpId);
                    if(res.StatusCode == HttpStatusCode.NoContent) 
                    {
                        return new ResponseModel { Status = true, Message = "Employee Updated Successfully" };
                    }
                    else
                    {
                        // Handle other status codes
                        return new ResponseModel { Status = false, Message = $"Failed to create employee. Status code: {res.StatusCode}" };
                    }
                }
                else
                {
                    return new ResponseModel { Status = false, Message = "Employee not found" };
                }
            }
            catch(Exception ex) 
            {
                return new ResponseModel { Status = false, Message = "An error occurred while processing your request" };
            }
        }

        public async Task<Employee> DeleteEmp(int id)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"api/employee/{id}");

            res.EnsureSuccessStatusCode();

            string responseContent = await res.Content.ReadAsStringAsync();

            Employee deleteEmp = JsonConvert.DeserializeObject<Employee>(responseContent);

            return deleteEmp;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // ADDRESS SERVICE
        public async Task<List<Address>> GetAddresses()
        {
            var addresses = await _httpClient.GetFromJsonAsync<List<Address>>("api/address/GetAllAddress");
            return addresses;
        }

        public async Task<Address> GetAddressById(int addressId) 
        {
            var addId =  await _httpClient.GetFromJsonAsync<Address>($"api/address/{addressId}");
            if(addId!=null)
            {
                return addId;
            }
            return null;
        }
    }
}
