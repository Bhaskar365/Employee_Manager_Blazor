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
            return await _httpClient.GetFromJsonAsync<List<Employee>>("api/employee/GetAll");
        }

        public async Task<List<Employee>> GetEmployeeById(int id) 
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/{id}");
        }

        public async Task<Employee> CreateEmp(Employee emp) 
        {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", emp);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Employee createdEmp = JsonConvert.DeserializeObject<Employee>(responseContent);
                        return createdEmp;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle bad request error (e.g., invalid input)
                        // You can choose how to handle this scenario (throw an exception, return null, etc.)
                        // Example: throw new CustomException("Bad request");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        throw new CustomException($"Bad request: {responseContent}");
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Handle not found error (e.g., resource not found)
                        // You can choose how to handle this scenario (throw an exception, return null, etc.)
                        // Example: throw new CustomException("Resource not found");
                        string responseContent = await response.Content.ReadAsStringAsync();
                        throw new CustomException($"Bad request: {responseContent}");
                    }
                    else
                    {
                        // Handle other HTTP status codes (e.g., 500 Internal Server Error)
                        // You can choose how to handle this scenario (throw an exception, return null, etc.)
                        // Example: throw new CustomException("Internal Server Error");
                        throw new Exception($"Internal Server Error, {StatusCodes.Status500InternalServerError}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Handle network-related errors (e.g., server not reachable)
                    // Example: throw new CustomException("Network error");
                    throw new CustomException("Network error");
                }
                catch (Exception ex)
                {
                    // Handle other unexpected exceptions
                    // Example: throw new CustomException("Unexpected error");
                    throw new CustomException("Unexpected error");
                }
            
                // Return null or throw an exception as appropriate for your error handling strategy. 
                return null;         


            //HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", emp);
            //response.EnsureSuccessStatusCode();

            //string responseContent = await response.Content.ReadAsStringAsync();
            //Employee createdEmp = JsonConvert.DeserializeObject<Employee>(responseContent);

            //return createdEmp;
        }

        public async Task<Employee> DeleteEmp(int id) 
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"api/employee/{id}");

            res.EnsureSuccessStatusCode();

            string responseContent = await res.Content.ReadAsStringAsync();

            Employee deleteEmp = JsonConvert.DeserializeObject<Employee>(responseContent);

            return deleteEmp;
        }

        public class CustomException : Exception
        {
            public CustomException(string message) : base(message)
            {
                throw new Exception(message);
            }
        }

    }
}
