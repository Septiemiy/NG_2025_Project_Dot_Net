using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeFuncs.Functions.Command.Models
{
    public class ThresholdDTO
    {
        public string DeviceId { get; set; }
        public string ThresholdCondition { get; set; }
        public string Value { get; set; }
    }
}
