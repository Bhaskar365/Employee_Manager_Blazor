
using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;
using Employee_Manager_Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;


namespace Employee_Manager_Client.Services
{
    public class FrontendUserPanelService : IFrontendUserPanelService
    {
        private readonly HttpClient _httpClient;
        private readonly ProtectedSessionStorage _sessionStorage;
        public FrontendUserPanelService(ProtectedSessionStorage sessionStorage, HttpClient httpclient)
        {
            _sessionStorage = sessionStorage;
            _httpClient = httpclient;
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public async Task<string> ExportToExcelForAllData()
        {
            var response = await _httpClient.GetAsync("api/ExportOpenXML");
            if (response.IsSuccessStatusCode)
            {
                // Get the response content as a string

                var x = response.Content.Headers.ContentDisposition.FileName;

                //string responseBody = await response.Content.ReadAsStringAsync();
                string responseBody =  response.RequestMessage.RequestUri.AbsoluteUri.ToString();

                // Extract the download link from the response body
                string downloadLink = responseBody; // Trim any surrounding double quotes

                return downloadLink;
            }
            else
            {
                throw new Exception("Failed to get download link from API.");
            }
        }

        public async Task<bool> IsUserLoggedIn()
        {
            bool flag = false;
            var result = await _sessionStorage.GetAsync<string>("adminKey");
            if (result.Success)
            {
                flag = true;
            }
            return flag;
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
            ResponseModel result = await response.Content.ReadFromJsonAsync<ResponseModel>();

            return result;
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
                if (emp != null)
                {
                    HttpResponseMessage res = await _httpClient.PutAsJsonAsync($"api/employee/{emp.EmpId}", emp);
                    if (res.StatusCode == HttpStatusCode.NoContent)
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseModel> DeleteEmp(int id)
        {
            try
            {
                if (id != null)
                {
                    HttpResponseMessage res = await _httpClient.DeleteAsync($"api/employee/{id}");

                    if (res.StatusCode == HttpStatusCode.NoContent)
                    {
                        return new ResponseModel { Status = true, Message = "Employee Updated Successfully" };
                    }
                    else
                    {
                        return new ResponseModel { Status = false, Message = $"Failed to create employee. Status code: {res.StatusCode}" };
                    }
                }
                else
                {
                    return new ResponseModel { Status = false, Message = "Employee not found" };
                }
            }
            catch (Exception ex)
            {
                return null;
            }

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
            var addId = await _httpClient.GetFromJsonAsync<Address>($"api/address/{addressId}");
            if (addId != null)
            {
                return addId;
            }
            return null;
        }
    }
}
