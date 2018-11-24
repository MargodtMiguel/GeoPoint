using GeoPoint.Models;
using GeoPoint.Models.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GeoPoint.API.Hubs
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("CorsPolicy")]
    public class friendRequest : Hub
    {
        public friendRequest()
        {
        }

        public override async Task OnConnectedAsync() {
           
            await Clients.Caller.SendAsync("Login", new { message = $"Welcome {Context.ConnectionId}!" });
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public async Task Login(string username) {
            await Clients.Caller.SendAsync("ServerMessage", new { userName = username, message = $"Welcome {username}!" });
            await Clients.Others.SendAsync("ServerMessage", new { message = $"{username} just logged in." });
        }
        public async Task sendFriendRequest(string friendusername,string username)
        {
            await Clients.Others.SendAsync("RecieveFriendRequest",username,friendusername);
        }
        
      


    }

}
