using Refit;
using SentinelBLL.Clients;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryClient _categoryClient;

        public CategoryService(ICategoryClient categoryClient)
        {
            _categoryClient = categoryClient;
        }

        public async Task<ICollection<CategoryDTO>> GetAllCategoriesAsync()
        {
            try
            {
                return await _categoryClient.GetAllCategoriesAsync();
            }
            catch (ApiException ex)
            {
                return null;
            }
        }

        public async Task<string> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            try
            {
                return await _categoryClient.AddCategoryAsync(categoryDTO);
            }
            catch (ApiException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ChangeCategoryNameAsync(CategoryDTO categoryDTO)
        {
            try
            {
                return await _categoryClient.ChangeCategoryNameAsync(categoryDTO);
            }
            catch (ApiException ex)
            {
                return ex.Message;
            }
        }
    }
}
