using Employee_Manager_API.Pagination;
using Employee_Manager_API.Pagination.Paging;
using Employee_Manager_Models;

namespace Employee_Manager_API.Pagination.Interface
{
    public interface IProductRepository
    {
        Task<PagedList<Employee>> GetProducts(ProductParameters productParameters);
    }
}
