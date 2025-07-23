using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : Controller
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
        _logger.LogInformation("[INFO][DeviceGateway]Fetching all categories");
        var response = await _categoryService.GetCategoriesAsync();

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("[INFO][DeviceGateway]Successfully fetched categories");
            var categories = await response.Content.ReadFromJsonAsync<ICollection<CategoryDTO>>();
            return Ok(categories);
        }

        _logger.LogError("[ERROR][DeviceGateway]Failed to fetch categories");
        return BadRequest(new { Message = "Failed to get categories" });
    }

    [HttpPost("changeCategoryName")]
    public async Task<IActionResult> ChangeCategoryName([FromBody] CategoryDTO categoryDTO)
    {   
        _logger.LogInformation("[INFO][DeviceGateway]Changing category name for category ID: {CategoryId}", categoryDTO.CategoryId);
        var response = await _categoryService.ChangeCategoryName(categoryDTO);
        
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("[INFO][DeviceGateway]Category name changed successfully for category ID: {CategoryId}", categoryDTO.CategoryId);
            return Ok(new { Message = "Category name changed successfully" });
        }

        _logger.LogError("[ERROR][DeviceGateway]Failed to change category name for category ID: {CategoryId}", categoryDTO.CategoryId);
        return BadRequest(new { Message = "Failed to change category name" });
    }

    [HttpPost("addCategory")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        _logger.LogInformation("[INFO][DeviceGateway]Creating new category with name: {CategoryName}", categoryDTO.Name);
        var response = await _categoryService.CreateCategoryAsync(categoryDTO);
        
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("[INFO][DeviceGateway]Category created successfully with ID: {CategoryId}", categoryDTO.CategoryId);
            return Ok(new { Message = "Category created successfully" });
        }

        _logger.LogError("[ERROR][DeviceGateway]Failed to create category with name: {CategoryName}", categoryDTO.Name);
        return BadRequest(new { Message = "Failed to create category" });
    }
}
