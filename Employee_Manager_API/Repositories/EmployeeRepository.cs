﻿using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        public EmployeeRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ICollection<Employee> GetAllEmployees()
        {
            var employees = _context.Tbl_Employee
                .Include(e => e.Address)
                .Include(e => e.Department)
                .ToList();

            return employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Tbl_Employee.Where(e => e.EmpId == id).FirstOrDefault();
        }

        public bool EmployeeExists(int id)
        {
            return _context.Tbl_Employee.Any(e => e.EmpId == id);
        }

        public bool CreateEmployee(Employee emp)
        {
            // Retrieve the department associated with the employee
            var department = _context.Tbl_Department.FirstOrDefault(d => d.DepartmentId == emp.DepartmentId);

            if (department != null)
            {
                // Associate the retrieved department with the employee
                emp.Department = department;

                // Set the CreatedOn property
                emp.CreatedOn = DateTime.UtcNow;

                // Add the employee to the context
                _context.Add(emp);

                // Save changes to the database
                return Save();
            }
            else
            {
                // Department not found, handle accordingly (throw an exception, return false, etc.)
                return false;
            }
        }

        public bool UpdateEmployee(Employee emp, int depID)
        {
            var department = _context.Tbl_Department.FirstOrDefault(d => d.DepartmentId == depID);
            if(department!= null) 
            {
                emp.Department = department;
            }
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


