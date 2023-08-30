using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Employee_Manager_API.DbClass;
using Employee_Manager_API.Models;
using Employee_Manager_Models;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    [EnableCors("AllowOrigin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly EmpDbClass dbContext;
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IConfiguration config, EmpDbClass dbContext, IEmployeeRepository employeeRepository)
        {
            this.config = config;
            this.dbContext = dbContext;
            this.employeeRepository = employeeRepository;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            try
            {
                return (await employeeRepository.GetAllEmployees()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployeeById(id);

                if (result == null)
                    return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee emp)
        {
            try
            {
                if (emp == null)
                    return BadRequest();

                var createdEmp = await employeeRepository.AddEmployee(emp);

                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmp.Id }, createdEmp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee emp, int id)
        {
            try
            {
                if (id != emp.Id)
                    return BadRequest("Employee ID mismatch");

                var empToUpdate = await employeeRepository.GetEmployeeById(id);

                if (empToUpdate == null)
                    return NotFound($"Employee with id = {id} not found");

                return await employeeRepository.UpdateEmployee(emp);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id) 
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployeeById(id);

                if (employeeToDelete == null) 
                {
                    return NotFound($"Employee with id = {id} not found");
                }

                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
