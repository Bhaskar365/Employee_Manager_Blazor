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

            return Ok("Successfully added");
        }

        //[HttpPost("InsertEmployee")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult InsertEmployee(Employee employee) 
        //{
        //    employee.FirstName = employee.FirstName.Trim();
        //    employee.LastName = employee.LastName.Trim();
        //    employee.Email = employee.Email.Trim();
        //    employee.Gender = employee.Gender.Trim();
        //    employee.Department.DepartmentName = employee.Department.DepartmentName.ToUpper();
        //    employee.Department.DepartmentId = employee.Department.DepartmentId;
        //    employee.Address.Street = employee.Address.Street;
        //    employee.Address.Country = employee.Address.Country;
        //    employee.Address.State = employee.Address.State;
        //    employee.DOB = employee.DOB;
        //    employee.JoiningDate = employee.JoiningDate;

        //    _employeeRepository.InsertNewEmployee(employee);
        //    return Ok("Inserted employee successfully");
        //}

        [HttpPut("{empId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int empId, [FromQuery] int depID, [FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            var dep = _departmentRepository.GetDepartment(depID);

            if (dep.DepartmentId != depID)
                return BadRequest(ModelState);

            if (_departmentRepository.DepartmentExists(depID))
                return NotFound();

            if (empId != employee.EmpId)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            if (_employeeRepository.UpdateEmployee(employee, depID))
            {
                ModelState.AddModelError("", "Something went wrong while updating owner");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

