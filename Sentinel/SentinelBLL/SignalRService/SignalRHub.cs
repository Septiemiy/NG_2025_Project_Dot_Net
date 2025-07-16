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
        public async Task Subscribe(Guid deviceId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"device-{deviceId}");
            return;
        }

        public async Task Unsubscribe(Guid deviceId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"device-{deviceId}");
            return;
        }
    }
}
