using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        if (categories == null)
        {
            return NotFound(new { message = "Error get categories" });
        }

        return Ok(categories);
    }

    
    [HttpPost("addCategory")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
    {
        categoryDTO.CategoryId = Guid.NewGuid().ToString();

        if (categoryDTO == null || string.IsNullOrEmpty(categoryDTO.Name))
        {
            return BadRequest(new { message = "Invalid category data" });
        }
        
        var result = await _categoryService.AddCategoryAsync(categoryDTO);

        return Ok(new { message = result });
    }

    
    [HttpPost("changeCategoryName")]
    public async Task<IActionResult> ChangeCategoryName([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO == null || string.IsNullOrEmpty(categoryDTO.Name))
        {
            return BadRequest(new { message = "Invalid category data" });
        }
        
        var result = await _categoryService.ChangeCategoryNameAsync(categoryDTO);
        
        return Ok(new { message = result });
    }
}
