using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmpDbClass _context;
        public DepartmentRepository(EmpDbClass context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            return await _context.Department.ToListAsync();
        }

        public async Task<Department> GetDepartment(int depID)
        {
            return await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == depID.ToString());
        }

        public async Task<Department> AddDepartment(Department dep)
        {
            var result = await _context.Department.AddAsync(dep);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Department> UpdateDepartment(Department updateDep)
        {
            var existingDepartment = await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == updateDep.DepartmentId);
            if(existingDepartment == null) 
            {
                return null;
            }
            existingDepartment.DepartmentName = updateDep.DepartmentName;
            existingDepartment.DepartmentId = updateDep.DepartmentId;

            await _context.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<Department> DeleteDepartment(Department dep)
        {
            var doesDepartmentExist = await _context.Department.FirstOrDefaultAsync(x => x.DepartmentId == dep.DepartmentId);

            if (doesDepartmentExist == null)
                return null;

            _context.Department.Remove(doesDepartmentExist);
            await _context.SaveChangesAsync();
            return doesDepartmentExist;
        }
    }
}