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

public class HttpDeviceAddToTS
{
    private const string PartitionKey = "Devices";
    private const string TableName = "RegisteredDevices";

    [Function(nameof(HttpDeviceAddToTS))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "device/register")] 
        HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        
        var registerDeviceDTO = JsonSerializer.Deserialize<RegisterDeviceDTO>(requestBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        try
        {
            var entity = new DeviceEntity
            {
                PartitionKey = PartitionKey,
                RowKey = registerDeviceDTO.DeviceId,
                Name = registerDeviceDTO.Name,
                Description = registerDeviceDTO.Description,
                Type = registerDeviceDTO.Type,
                Location = registerDeviceDTO.Location,
                CategoryId = registerDeviceDTO.CategoryId,
                UserId = registerDeviceDTO.UserId,
                CreatedAt = registerDeviceDTO.CreatedAt
            };
            var table = await GetTableStorage.GetTableAsync(TableName);
            await table.ExecuteAsync(TableOperation.Insert(entity));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding device to table storage: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new OkObjectResult($"Device added successfully");
    }
}