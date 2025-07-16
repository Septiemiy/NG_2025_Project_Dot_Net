using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using SmartHomeFuncs.Functions.TableStorage.Entity;
using SmartHomeFuncs.Functions.TableStorage.Models;
using SmartHomeFuncs.Functions.TableStorage.Services;

namespace SmartHomeFuncs.Functions.TableStorage;

public class HttpGetCategoriesFromTS
{
    private const string TableName = "Categories";

    [Function(nameof(HttpGetCategoriesFromTS))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "category/getAll")]
        HttpRequest req)
    {
        var categories = new List<CategoryDTO>();

        try
        {
            var table = await GetTableStorage.GetTableAsync(TableName);
            var query = new TableQuery<CategoryEntity>();

            foreach (var entity in await table.ExecuteQuerySegmentedAsync(query, null))
            {
                categories.Add(new CategoryDTO
                {
                    CategoryId = entity.RowKey,
                    Name = entity.Name
                });
            }

            return new OkObjectResult(categories);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting categories from table storage: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}