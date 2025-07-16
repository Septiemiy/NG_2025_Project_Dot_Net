using DeviceGatewayBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<HttpResponseMessage> GetCategoriesAsync();
        Task<HttpResponseMessage> ChangeCategoryName(CategoryDTO categoryDTO);
        Task<HttpResponseMessage> CreateCategoryAsync(CategoryDTO categoryDTO);
    }
}
