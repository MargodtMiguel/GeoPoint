using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public interface IScoreRepo
    {
        Task<Score> CreateScore(Score score);
        Task<IEnumerable<Score>> GetTopScores(string area, int length);
        Task<Score> getOldScore(Score score);
        Task<Score> UpdateScore(Score score);
        bool ScoreExists(Score s);
        Task<MongoModels.Score> CreateAsync(MongoModels.Score s);
        Task<IEnumerable<MongoModels.Score>> GetMongoScores();
    }
}