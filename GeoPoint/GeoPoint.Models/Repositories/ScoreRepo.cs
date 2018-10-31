using GeoPoint.Models.Data;
using Microsoft.EntityFrameworkCore;
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
        public ScoreRepo(GeoPointAPIContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Score>> GetTopScores(string area,int length)
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
            catch(Exception e)
            {
                throw (e);
            }
        }
        public async Task<Score> UpdateScore(Score score)
        {
            try
            {
                _context.Scores.Update(score);
                await _context.SaveChangesAsync();
                return score;
            }
            catch(Exception e)
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
    }
}
