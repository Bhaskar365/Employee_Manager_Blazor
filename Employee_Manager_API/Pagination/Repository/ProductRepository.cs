using Employee_Manager_API.DbClass;
using Employee_Manager_API.Pagination;
using Employee_Manager_API.Pagination.Interface;
using Employee_Manager_API.Pagination.Paging;
using Employee_Manager_Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Manager_API.Pagination.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public async Task<PagedList<Employee>> GetProducts(ProductParameters productParameters)
        {
            var products = await _context.Tbl_Employee.ToListAsync();

            return PagedList<Employee>.ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }
    }
}
