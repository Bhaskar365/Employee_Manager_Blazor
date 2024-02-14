using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAddressRepository _addressRespository;
        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IDepartmentRepository departmentRepository,
                                  IAddressRepository addressRespository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _addressRespository = addressRespository;
        }

        [HttpGet("GetAllEmployees")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        [HttpGet("{empID}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int empID)
        {
            if (!_employeeRepository.EmployeeExists(empID))
                return NotFound();

            var employee = _employeeRepository.GetEmployee(empID);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] Employee emp)
        {
            if (emp == null)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(emp.DepartmentId))
                return BadRequest("Invalid DepartmentId");

            emp.CreatedOn = DateTime.UtcNow;

            // Retrieve the associated department
            var department = _departmentRepository.GetDepartment(emp.DepartmentId);
            if (department == null)
            {
                ModelState.AddModelError("", "Department not found");
                return BadRequest(ModelState);
            }

            // Associate the department with the employee
            emp.Department.DepartmentId = department.DepartmentId;
            emp.Department.DepartmentName = department.DepartmentName;

            // Save the employee
            if (!_employeeRepository.CreateEmployee(emp))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtAction(nameof(GetEmployee), new { id = emp.EmpId }, emp);
            return NoContent();
        }

        [HttpPut("{empId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int empId, [FromQuery] int depID, [FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            depID = employee.DepartmentId;

            var dep = _departmentRepository.GetDepartment(depID);

            if (dep.DepartmentId != depID)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(depID))
                return NotFound();

            if (empId != employee.EmpId)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_employeeRepository.UpdateEmployee(employee, depID))
            {
                ModelState.AddModelError("", "Something went wrong while updating employee");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{empId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int empId)
        {
            if (!_employeeRepository.EmployeeExists(empId))
                return NotFound();

            var employeeToDelete = _employeeRepository.GetEmployee(empId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_employeeRepository.DeleteEmployee(employeeToDelete)) 
            {
                ModelState.AddModelError("", "Something went wrong deleting employee");
            }
            return NoContent();
        }

        [HttpGet("{searchTerm}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string query)
        {
            try 
            {
                var result = await _employeeRepository.Search(query);

                if(result.Any()) 
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}

