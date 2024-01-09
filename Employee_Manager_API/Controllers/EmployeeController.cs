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
        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
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
        public IActionResult GetEmployee(int id)
        {
            if (!_employeeRepository.EmployeeExists(id))
                return NotFound();

            var employee = _employeeRepository.GetEmployee(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] Employee emp, [FromQuery] Department department)
        {
            var departmentValue = _departmentRepository.GetAllDepartments().Where(d => d.DepartmentId.ToString() == emp.EmpId);

            if (department == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return StatusCode(422, ModelState);

            if (!_employeeRepository.CreateEmployee(emp))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added");
        }
    }
}
