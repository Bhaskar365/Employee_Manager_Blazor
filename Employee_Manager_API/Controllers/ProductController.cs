using Employee_Manager_API.DbClass;
using Employee_Manager_API.Interfaces;
using Employee_Manager_API.Pagination;
using Employee_Manager_API.Pagination.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Employee_Manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            productRepo = _productRepo;
        }
        public async Task<IActionResult> Get([FromQuery] ProductParameters productParameters)
        {
            var products = await _productRepo.GetProducts(productParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));
            return Ok(products);
        }
    }
}
