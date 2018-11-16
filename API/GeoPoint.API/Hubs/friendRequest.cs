using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.Hubs
{
    public class friendRequest : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync() {
            await Clients.Caller.SendAsync("Login", new { message = $"Welcome {Context.ConnectionId}!" });
            await base.OnConnectedAsync();
        }
    }
}
