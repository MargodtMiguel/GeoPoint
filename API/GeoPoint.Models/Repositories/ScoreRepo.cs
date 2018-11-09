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
            return await collection.Find(FilterDefinition<Score>.Empty).ToListAsync<Score>();
        }

        public async Task<IEnumerable<Score>> GetTopScoresAsync(string area,int length)
        {
            return await mongoDBContext.Scores.Find(s => s.Area == area).SortByDescending(s => s.Value).ThenByDescending(s => s.TimeSpan).Limit(length).ToListAsync<Score>();
            
        }

        #endregion
    }
}
