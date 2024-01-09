using Employee_Manager_Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.DbClass
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Tbl_Employee { get; set; }
        public DbSet<Department> Tbl_Department { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .IsRequired();
        }
    }
}