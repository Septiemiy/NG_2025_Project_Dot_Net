using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeFuncs.Functions.TableStorage.Services
{
    public static class GetTableStorage
    {
        public static async Task<CloudTable> GetTableAsync(string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("StorageAccountConnectionString"));
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}
