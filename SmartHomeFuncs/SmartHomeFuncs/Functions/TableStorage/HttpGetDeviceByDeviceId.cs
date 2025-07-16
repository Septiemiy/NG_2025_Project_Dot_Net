using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using SmartHomeFuncs.Functions.TableStorage.Entity;
using SmartHomeFuncs.Functions.TableStorage.Models;
using SmartHomeFuncs.Functions.TableStorage.Services;

namespace SmartHomeFuncs.Functions.TableStorage;

public class HttpGetDeviceByDeviceId
{
    private const string PartitionKey = "Devices";
    private const string TableName = "RegisteredDevices";

    [Function(nameof(HttpGetDeviceByDeviceId))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route= "device/get/{deviceId}")]
        HttpRequest req,
        string deviceId)
    {
        try
        {
            var table = await GetTableStorage.GetTableAsync(TableName);
            var retrieveOperation = TableOperation.Retrieve<DeviceEntity>(PartitionKey, deviceId);
            var result = await table.ExecuteAsync(retrieveOperation);

            if (result.Result is DeviceEntity deviceEntity)
            {
                return new OkObjectResult(new RegisterDeviceDTO
                {
                    DeviceId = deviceEntity.RowKey,
                    Name = deviceEntity.Name,
                    Description = deviceEntity.Description,
                    Type = deviceEntity.Type,
                    Location = deviceEntity.Location,
                    CategoryId = deviceEntity.CategoryId,
                    UserId = deviceEntity.UserId,
                    CreatedAt = deviceEntity.CreatedAt
                });
            }
            else
            {
                return new NotFoundResult();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting device by ID: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}