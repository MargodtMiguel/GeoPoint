using GeoPoint.Models.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{

    public class UserRepo : IUserRepo
    {
        private readonly GeoPointAPIMongoDBContext mongoDBContext;
        public UserRepo(GeoPointAPIMongoDBContext mongoDBContext)
        {
            this.mongoDBContext = mongoDBContext;
        }

        public async Task<bool> checkIfFriends(GeoPointUser curUser, string friend)
        {
            var filter = Builders<GeoPointUser>.Filter.ElemMatch(us => us.Friends, fr => fr.Username.Equals(friend));
            var result = await mongoDBContext.Users.Find(filter).FirstOrDefaultAsync();
            if (result != null)
            {
                return true;
            }
            return false;
        }
        public async Task sendFriendRequest(GeoPointUser friend, Friend f)
        {
            var filterResult = Builders<GeoPointUser>.Filter.Eq(b => b.Id, friend.Id);
            await mongoDBContext.Users.UpdateOneAsync(filterResult, Builders<GeoPointUser>.Update.Push(l => l.Friends, f));
        }
    }
}