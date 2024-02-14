using Employee_Manager_API.Pagination;
using Employee_Manager_Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Employee_Manager_Client.Pages.Pagination
{
    public class ProductHttpRepository : IProductHttpRepository
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ProductHttpRepository(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<PagingResponse<Employee>> GetProducts(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("products", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<Employee>
            {
                Items = JsonSerializer.Deserialize<List<Employee>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }
    }
}