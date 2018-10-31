using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public interface IFriendsRepo
    {
        Task<Friends> ConfirmFriends(Friends f);
        Task<Friends> RemoveFriend(Friends f);
        Task<Friends> SendFriendRequest(Friends f);
        bool FriendExist(Friends f);
    }
}