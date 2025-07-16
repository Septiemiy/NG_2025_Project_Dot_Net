using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service.Interface
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDTO>> GetAllCategoriesAsync();
        Task<string> AddCategoryAsync(CategoryDTO categoryDTO);
        Task<string> ChangeCategoryNameAsync(CategoryDTO categoryDTO);
    }
}
