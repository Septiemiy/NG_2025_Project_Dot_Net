using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        var response = await _categoryService.GetCategoriesAsync();

        if (response.IsSuccessStatusCode)
        {
            var categories = await response.Content.ReadFromJsonAsync<ICollection<CategoryDTO>>();
            return Ok(categories);
        }

        return BadRequest(new { Message = "Failed to get categories" });
    }

    [HttpPost("changeCategoryName")]
    public async Task<IActionResult> ChangeCategoryName([FromBody] CategoryDTO categoryDTO)
    {   
        var response = await _categoryService.ChangeCategoryName(categoryDTO);
        
        if (response.IsSuccessStatusCode)
        {
            return Ok(new { Message = "Category name changed successfully" });
        }

        return BadRequest(new { Message = "Failed to change category name" });
    }

    [HttpPost("addCategory")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        var response = await _categoryService.CreateCategoryAsync(categoryDTO);
        
        if (response.IsSuccessStatusCode)
        {
            return Ok(new { Message = "Category created successfully" });
        }
        
        return BadRequest(new { Message = "Failed to create category" });
    }
}
