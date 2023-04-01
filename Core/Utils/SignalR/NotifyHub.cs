using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.SignalR
{
    public class NotifyHub : Hub
    {
        public Task SendBroadcastMessage(string message) 
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
