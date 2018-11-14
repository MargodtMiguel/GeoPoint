using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public interface IUserRepo
    {
        Task<bool> checkIfFriends(GeoPointUser curUser, string friend);
        Task sendFriendRequest(GeoPointUser friend,Friend f);
    }
}