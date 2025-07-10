using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SentinelBLL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ConditionsStatus
    {
        Offline = 0,
        Online = 1
    }
}
