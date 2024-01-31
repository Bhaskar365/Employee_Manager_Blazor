using Employee_Manager_Models;
using Employee_Manager_Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.DbClass
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Tbl_Employee { get; set; }
        public DbSet<Department> Tbl_Department { get; set; }
        public DbSet<Address> Tbl_Address { get; set; }
        public DbSet<AdminInfo> AdminInfo { get; set; }
    }
}