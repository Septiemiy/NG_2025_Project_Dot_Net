using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CategoriesClient");
        }

        public async Task<HttpResponseMessage> ChangeCategoryName(CategoryDTO categoryDTO)
        {
            return await _httpClient.PostAsJsonAsync("category/changeName", categoryDTO);
        }

        public async Task<HttpResponseMessage> CreateCategoryAsync(CategoryDTO categoryDTO)
        {
            return await _httpClient.PostAsJsonAsync("category/addCategory", categoryDTO);
        }

        public async Task<HttpResponseMessage> GetCategoriesAsync()
        {
            return await _httpClient.GetAsync("category/getAll");
        }
    }
}
