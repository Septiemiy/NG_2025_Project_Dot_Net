﻿using DAL_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Core.Entities
{
    public class Threshold : BaseEntity
    {
        public Guid DeviceId { get; set; }
        public string ThresholdCondition { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
        public ConditionsStatus Status { get; set; }
    }
}
