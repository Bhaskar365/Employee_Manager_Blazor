using Employee_Manager_Models;
using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

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

        public async Task<List<Employee>> GetEmployeeById(int empID)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/{empID}");
        }

        public async Task<bool> CreateEmp(Employee emp)
        {
            try
            {
                Department department = await GetDepartment(emp.DepartmentId);

                if (department != null)
                {
                    emp.Department = department;

                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", emp);

                    if (response.IsSuccessStatusCode)
                    {
                        //string responseContent = await response.Content.ReadAsStringAsync();
                        //Employee createdEmp = JsonConvert.DeserializeObject<Employee>(responseContent);
                        //return true;
                        string responseContent = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(responseContent))
                        {
                            Employee createdEmp = JsonConvert.DeserializeObject<Employee>(responseContent);
                            return true;
                        }
                        else {
                            // Handle the case where the response content is empty or null
                            Console.WriteLine("Empty or null response content");
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Department not found");
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
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
    }


}
