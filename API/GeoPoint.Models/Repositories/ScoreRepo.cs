using GeoPoint.Models.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public class ScoreRepo : IScoreRepo
    {
        private readonly GeoPointAPIMongoDBContext mongoDBContext;
        public ScoreRepo( GeoPointAPIMongoDBContext mongoDBContext)
        {
            this.mongoDBContext = mongoDBContext; 
        }
       

        #region MongoDB

        public async Task<Score> CreateAsync(Score score)    {   
            await mongoDBContext.Scores.InsertOneAsync(score);     
            return score;
        }

        public async Task<IEnumerable<Score>> GetAllScoresAsync()
        {
            IMongoCollection<Score> collection = mongoDBContext.Database.GetCollection<Score>("scores");
            return await collection.Find(FilterDefinition<Score>.Empty)
                .ToListAsync<Score>();
        }

        public async Task<IEnumerable<Score>> GetTopScoresAsync(string area,int length)
        {          
            return await mongoDBContext.Scores.Find(s => s.Area == area)
                .SortByDescending(s => s.Value)
                .ThenBy(s => s.TimeSpan)
                .Limit(length)
                .ToListAsync<Score>();            
        }
        public async Task<IEnumerable<Score>> GetFriendTopScoresAsync(GeoPointUser user,string area, int length)
        {
            List<Score> TopScores = new List<Score>();
            foreach(Friend friend in user.Friends.ToList())
            {
                var filter = Builders<Score>.Filter.Where(s => s.User.UserName == friend.Username);
                filter = filter & Builders<Score>.Filter.Where(s => s.Area == area);
                TopScores.AddRange(await mongoDBContext.Scores.Find(filter).ToListAsync());
            }
            var filter2 = Builders<Score>.Filter.Where(s => s.User.UserName == user.UserName);
            filter2 = filter2 & Builders<Score>.Filter.Where(s => s.Area == area);
            TopScores.AddRange(await mongoDBContext.Scores.Find(filter2).ToListAsync());
            return TopScores.OrderByDescending(s => s.Value).ThenBy(s => s.TimeSpan).Take(length);
        }
        #endregion
    }
}
