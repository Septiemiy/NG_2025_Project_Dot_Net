using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using SmartHomeFuncs.Functions.TableStorage.Entity;
using SmartHomeFuncs.Functions.TableStorage.Models;
using SmartHomeFuncs.Functions.TableStorage.Services;

namespace SmartHomeFuncs.Functions.TableStorage;

public class HttpGetDevicesFromTS
{
    private const string TableName = "RegisteredDevices";

    [Function(nameof(HttpGetDevicesFromTS))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route="device/getAll")] 
        HttpRequest req)
    {
        var devices = new List<RegisterDeviceDTO>();

        try
        {
            var table = await GetTableStorage.GetTableAsync(TableName);
            var query = new TableQuery<DeviceEntity>();

            foreach (var entity in await table.ExecuteQuerySegmentedAsync(query, null))
            {
                devices.Add(new RegisterDeviceDTO
                {
                    DeviceId = entity.RowKey,
                    Name = entity.Name,
                    Description = entity.Description,
                    Type = entity.Type,
                    Location = entity.Location,
                    CategoryId = entity.CategoryId,
                    UserId = entity.UserId,
                    CreatedAt = entity.CreatedAt
                });
            }

            if (devices.Count == 0)
            {
                Console.WriteLine("No devices found in table storage.");
            }

            return new OkObjectResult(devices);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting devices from table storage: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}