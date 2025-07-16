using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using SmartHomeFuncs.Functions.TableStorage.Entity;
using SmartHomeFuncs.Functions.TableStorage.Models;
using SmartHomeFuncs.Functions.TableStorage.Services;
using System.Text.Json;

namespace SmartHomeFuncs.Functions.TableStorage;

public class HttpAddCategoryToTS
{
    private const string TableName = "Categories";
    private const string PartitionKey = "Category";

    [Function(nameof(HttpAddCategoryToTS))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route="category/addCategory")]
        HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var category = JsonSerializer.Deserialize<CategoryDTO>(requestBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        try
        {
            var entity = new CategoryEntity
            {
                PartitionKey = PartitionKey,
                RowKey = category.CategoryId,
                Name = category.Name
            };

            var table = await GetTableStorage.GetTableAsync(TableName);
            await table.ExecuteAsync(TableOperation.Insert(entity));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deserializing category: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new OkObjectResult($"Category added successfully");
    }
}