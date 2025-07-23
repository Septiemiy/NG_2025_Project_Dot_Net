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
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        if (categories == null)
        {
            _logger.LogError("[ERROR]: Failed to retrieve categories.");
            return NotFound(new { message = "Error get categories" });
        }

        _logger.LogInformation("[INFO]: Categories retrieved successfully: {Count} categories found", categories.Count);
        return Ok(categories);
    }

    
    [HttpPost("addCategory")]
    public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
    {
        categoryDTO.CategoryId = Guid.NewGuid().ToString();

        if (categoryDTO == null || string.IsNullOrEmpty(categoryDTO.Name))
        {
            _logger.LogError("[ERROR]: Invalid category data provided.");
            return BadRequest(new { message = "Invalid category data" });
        }
        
        _logger.LogInformation("[INFO]: Adding new category: {CategoryName}", categoryDTO.Name);
        var result = await _categoryService.AddCategoryAsync(categoryDTO);
        
        _logger.LogInformation("[INFO]: Category added successfully: {CategoryName}", categoryDTO.Name);
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
