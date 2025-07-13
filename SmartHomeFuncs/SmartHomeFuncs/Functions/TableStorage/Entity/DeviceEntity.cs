using Microsoft.WindowsAzure.Storage.Table;
using SmartHomeFuncs.Functions.TableStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeFuncs.Functions.TableStorage.Entity
{
    public class DeviceEntity : TableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DeviceEntity(string messageId, string deviceId, RegisterDeviceDTO registerDeviceDTO) 
        {
            PartitionKey = messageId;
            RowKey = deviceId;
            Name = registerDeviceDTO.Name;
            Description = registerDeviceDTO.Description;
            UserId = registerDeviceDTO.UserId;
            Type = registerDeviceDTO.Type;
            Location = registerDeviceDTO.Location;
        }
    }
}
