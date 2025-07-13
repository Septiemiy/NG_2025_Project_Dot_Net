using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class RegisterLoginResultDTO
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
