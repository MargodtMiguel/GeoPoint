using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            Debug.WriteLine(Context);
            await Clients.Caller.SendAsync("Login", new { message = $"Welcome {Context.ConnectionId}!" });
            await base.OnConnectedAsync();
        }
        public async Task sendFriendRequest(string user,string friend)
        {
            Debug.WriteLine(user, friend);
        }
    }
}
