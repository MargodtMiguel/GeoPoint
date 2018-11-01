using GeoPoint.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public interface IFriendsRepo
    {
        Task<List<PublicProfileVM>> GetFriendsByUserId(string UserId);
        Task<bool> ConfirmFriends(Friends f);
        Task<bool> RemoveFriend(Friends f);
        Task<Friends> SendFriendRequest(Friends f);
        bool FriendExist(Friends f);
    }
}