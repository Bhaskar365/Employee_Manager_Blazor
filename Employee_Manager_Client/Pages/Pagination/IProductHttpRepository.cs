using Employee_Manager_API.Pagination;
using Employee_Manager_Models;

namespace Employee_Manager_Client.Pages.Pagination
{
    public interface IProductHttpRepository
    {
        Task<PagingResponse<Employee>> GetProducts(ProductParameters productParameters);
    }
}
}
