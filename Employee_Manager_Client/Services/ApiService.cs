﻿using Employee_Manager_Models;
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

        public async Task<Employee> GetEmployees()
        {
            return await _httpClient.GetFromJsonAsync<Employee>("api/employee/Employees");
        }

        public async Task<List<Employee>> GetEmployeeById(int empID)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/{empID}");
        }

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

        public async Task<bool> CreateEmp(Employee emp)
        {
            try
            {
                // Retrieve the associated department
                Department department = await GetDepartment(emp.DepartmentId);

                if (department != null)
                {
                    // Associate the retrieved department with the employee
                    emp.Department = department;

                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/employee", emp);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        Employee createdEmp = JsonConvert.DeserializeObject<Employee>(responseContent);
                        return true;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Handle 400 Bad Request (validation errors, etc.)
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Bad Request: {response.ReasonPhrase}");
                        Console.WriteLine($"Error Content: {errorContent}");
                    }
                    else
                    {
                        // Handle other HTTP status codes
                        Console.WriteLine($"Unexpected Status Code: {response.StatusCode}");
                    }
                }
                else
                {
                    // Department not found, handle accordingly (throw an exception, return null, etc.)
                    Console.WriteLine("Department not found");
                }

                return false;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                // Handle HTTP request-related issues
                return false;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Exception: {ex.Message}");
                // Handle JSON deserialization issues
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                // Handle other exceptions
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
