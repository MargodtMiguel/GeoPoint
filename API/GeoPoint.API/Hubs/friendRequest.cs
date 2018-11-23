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
        private readonly UserManager<GeoPointUser> userManager;
        public friendRequest(UserManager<GeoPointUser> userManager)
        {
            this.userManager = userManager;
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
            GeoPointUser user = await userManager.FindByNameAsync(username);
            user.ConnectionId = Context.ConnectionId;
            await userManager.UpdateAsync(user);
            await Clients.Caller.SendAsync("ServerMessage", new { userName = user.UserName, message = $"Welcome {user.UserName}!" });
            await Clients.Others.SendAsync("ServerMessage", new { message = $"{user.UserName} just logged in." });
        }
        public async Task sendFriendRequest(string friendusername,string username)
        {
            GeoPointUser user = await userManager.FindByNameAsync(username);
            GeoPointUser friend = await userManager.FindByNameAsync(friendusername);
            var Client = Clients.User(friend.ConnectionId);
            await Clients.Others.SendAsync("RecieveFriendRequest",username,friendusername);
        }
        
      


    }

}
