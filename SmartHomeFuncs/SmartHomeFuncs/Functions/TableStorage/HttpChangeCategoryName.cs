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

public class HttpChangeCategoryName
{
    private const string TableName = "Categories";
    private const string PartitionKey = "Category";

    [Function(nameof(HttpChangeCategoryName))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route="category/changeName")]
        HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var category = JsonSerializer.Deserialize<CategoryDTO>(requestBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        try
        {
            var table = await GetTableStorage.GetTableAsync(TableName);
            var retrieveOperation = TableOperation.Retrieve<CategoryEntity>(PartitionKey, category.CategoryId);
            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            if (retrievedResult.Result is CategoryEntity entity)
            {
                entity.Name = category.Name;
                var updateOperation = TableOperation.Replace(entity);
                await table.ExecuteAsync(updateOperation);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving category from table storage: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new OkObjectResult($"Category name changed successfully");
    }
}