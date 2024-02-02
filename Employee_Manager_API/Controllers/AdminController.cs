
using Employee_Manager_API.DbClass;
using Employee_Manager_Logic.Services;
using Employee_Manager_Models.CustomModels;
using Employee_Manager_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly AppDbContext _context;
        public AdminController(IAdminService adminService, AppDbContext context) 
        {
            _adminService = adminService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel login) 
        {
            var data = _adminService.AdminLogin(login);
            return Ok(data);
        }

        [HttpPost("Create")]
        public IActionResult Register(AdminInfo create)
        {
            create.CreatedOn = DateTime.Now.ToString();
            var data = _adminService.AdminRegister(create);
            //_context.AdminRegister.Add(create);
            //_context.SaveChanges();
            return Ok(data);
        }
    }
}
