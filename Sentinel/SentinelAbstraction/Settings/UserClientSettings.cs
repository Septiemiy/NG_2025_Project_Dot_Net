using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelAbstraction.Settings
{
    public class UserClientSettings
    {
        public const string SectionName = "RefitClients:UserClient";

        public string BaseAddress { get; set; } = string.Empty;
    }
}
