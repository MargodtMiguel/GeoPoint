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
            GeoPointUser user = await userManager.FindByNameAsync(Context.User.Identity.Name);
            user.ConnectionId = Context.ConnectionId;
            await userManager.UpdateAsync(user);
            await Clients.Caller.SendAsync("ServerMessage", new { message = $"Welcome {Context.ConnectionId}!" });
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            GeoPointUser user = await userManager.FindByNameAsync(Context.User.Identity.Name);
            user.ConnectionId = "";
            await base.OnDisconnectedAsync(exception);
        }
        public async Task sendFriendRequest(string username)
        {
            GeoPointUser friend = await userManager.FindByNameAsync(username);

            await Clients.User(friend.ConnectionId).SendAsync("RecieveFriendRequest",Context.User.Identity.Name);
        }
        
      


    }

}
