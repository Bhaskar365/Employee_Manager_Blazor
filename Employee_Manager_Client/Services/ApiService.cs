using Employee_Manager_Models;
using Newtonsoft.Json;
using System.Net;

namespace Employee_Manager_Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        
        public ApiService(HttpClient httpClient) 
        {
            this._httpClient = httpClient;
        }

        public async Task<List<Employee>> GetEmployees() 
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee/Employees");
        }

        public async Task<List<Employee>> GetEmployeeById(int empID) 
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/{empID}");
        }

        public async Task<Employee> CreateEmp(Employee emp) 
        {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", emp);
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Employee createdEmp = JsonConvert.DeserializeObject<Employee>(responseContent);

                    return createdEmp;
                }
                catch (Exception ex)
                {
                    return null;
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
