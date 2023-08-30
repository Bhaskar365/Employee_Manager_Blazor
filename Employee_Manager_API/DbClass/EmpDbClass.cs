using Employee_Manager_Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.DbClass
{
    public class EmpDbClass : DbContext
    {
        public EmpDbClass(DbContextOptions<EmpDbClass> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
    }
}
