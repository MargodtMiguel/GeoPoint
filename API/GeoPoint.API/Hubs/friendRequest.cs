using GeoPoint.Models;
using GeoPoint.Models.Data;
using Microsoft.AspNetCore.Http;
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
    public class friendRequest : Hub
    {
        private readonly GeoPointAPIMongoDBContext dBContext;
        public friendRequest(GeoPointAPIMongoDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task SendFriendRequest(string user,string friend)
        {
            try
            {
                CurUsers f = await getFriend(friend);
                if (f != null)
                {
                    await Clients.User(f.ConnectionID).SendAsync("RecieveFriendRequest", user + " has send you a friend request!");
                }
                else
                {
                    await Clients.Caller.SendAsync("ServerMessage", "user not online");
                }
            }
            catch(Exception e)
            {
                await Clients.Caller.SendAsync("ServerMessage", e.ToString());
            }
        }
        public override async Task OnConnectedAsync() {
            await Clients.Caller.SendAsync("Login", new { message = $"Welcome {Context.ConnectionId}!" });
            await base.OnConnectedAsync();
        }
        
        public async Task Login(string user) {
            await saveCurUser(new CurUsers { Username = user, ConnectionID = Context.ConnectionId });
            await Clients.Caller.SendAsync("ServerMessage", new { userName = user, message = $"Welcome {user}! - ConnectionId:"+ Context.ConnectionId });
            await Clients.Others.SendAsync("ServerMessage", new { message = $"{user} just logged in." });
        }

        public async Task saveCurUser(CurUsers curUser)
        {

            IMongoCollection<CurUsers> collection = dBContext.Database.GetCollection<CurUsers>("curusers");
            CurUsers c = await collection.Find(x => x.Username == curUser.Username).FirstOrDefaultAsync();
            if(c != null)
            {
                var result = await collection.ReplaceOneAsync(
                    filter: new BsonDocument("Username", curUser.Username),
                    options: new UpdateOptions { IsUpsert = true },
                    replacement: curUser);
            }
            else
            {
                await collection.InsertOneAsync(curUser);
            }
          
           
        }
        public async Task<CurUsers> getFriend(string f)
        {
            IMongoCollection<CurUsers> collection = dBContext.Database.GetCollection<CurUsers>("curusers");
            return await collection.Find(x => x.Username == f).FirstOrDefaultAsync();
        }


    }

}
