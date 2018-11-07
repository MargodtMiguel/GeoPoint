using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public interface IScoreRepo
    {
        Task<Score> CreateAsync(Score s);
        Task<IEnumerable<Score>> GetAllScoresAsync();
        Task<IEnumerable<Score>> GetTopScoresAsync( string area, int length);
    }
}