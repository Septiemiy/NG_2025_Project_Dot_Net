using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface ICategoryClient
    {
        [Get("/api/category/getAll")]
        Task<ICollection<CategoryDTO>> GetAllCategoriesAsync();

        [Post("/api/category/addCategory")]
        Task<string> AddCategoryAsync([Body] CategoryDTO categoryDTO);

        [Post("/api/category/changeCategoryName")]
        Task<string> ChangeCategoryNameAsync([Body] CategoryDTO categoryDTO);
    }
}
