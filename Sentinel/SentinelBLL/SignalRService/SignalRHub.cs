using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.SignalRService
{
    public class SignalRHub : Hub
    {
        public Task Subscribe(Guid deviceId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, $"device-{deviceId}");
        }

        public Task Unsubscribe(Guid deviceId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"device-{deviceId}");
        }
    }
}
