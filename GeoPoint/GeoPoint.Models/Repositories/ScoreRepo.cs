using GeoPoint.Models.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public class ScoreRepo : IScoreRepo
    {
        private readonly GeoPointAPIContext _context;
        private readonly GeoPointAPIMongoDBContext _mongocontext;
        public ScoreRepo(GeoPointAPIContext context, GeoPointAPIMongoDBContext mongocontext)
        {
            _context = context;
            _mongocontext = mongocontext; 
        }
        #region SQLDB
        public async Task<IEnumerable<Score>> GetTopScores(string area, int length)
        {
            var Scores = await this._context.Scores.Where(x => x.Area == area).OrderByDescending(x => x.Value).Take(length).AsNoTracking().ToListAsync();
            return Scores;
        }

        public async Task<Score> CreateScore(Score score)
        {
            try
            {
                _context.Scores.Add(score);
                await _context.SaveChangesAsync();
                return score;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public async Task<Score> UpdateScore(Score score)
        {
            try
            {
                var local = _context.Set<Score>().Local.FirstOrDefault(entry => entry.Id.Equals(score.Id));
                if (!(local == null))
                {
                    _context.Entry(local).State = EntityState.Detached;
                }
                _context.Entry(score).State = EntityState.Modified;
                _context.SaveChanges();
                _context.Scores.Update(score);
                await _context.SaveChangesAsync();
                return score;
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
        public async Task<Score> getOldScore(Score score)
        {
            return await _context.Scores.Where(x => x.Area == score.Area && x.UserId == score.UserId).FirstOrDefaultAsync();
        }
        public bool ScoreExists(Score s)
        {
            return _context.Scores.Any(x => x.Area == s.Area && x.UserId == s.UserId);
        }
        #endregion

        #region NoSQLDB
        public async Task<MongoModels.Score> CreateAsync(MongoModels.Score s)    {   
            await _mongocontext.Scores.InsertOneAsync(s);     
            return s;
        }

        //public async Task<IEnumerable<MongoModels.Score>> GetMongoScores()
        //{
        //    IMongoCollection<MongoModels.Score> collection = _mongocontext.Scores.
        //    return await collection.Find({ }).ToListAsync<MongoModels.Score>();
        //}
        #endregion
    }
}
