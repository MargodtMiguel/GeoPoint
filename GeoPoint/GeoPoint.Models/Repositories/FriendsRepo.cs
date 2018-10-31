using GeoPoint.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Repositories
{
    public class FriendsRepo : IFriendsRepo
    {
        private readonly GeoPointAPIContext _context;
        public FriendsRepo(GeoPointAPIContext context)
        {
            _context = context;
        }

        public async Task<Friends> SendFriendRequest(Friends f)
        {
            try
            {
                _context.Friends.Add(f);
                await _context.SaveChangesAsync();
                return f;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        public async Task<Friends> ConfirmFriends(Friends f)
        {
            try
            {
                _context.Friends.Update(f);
                await _context.SaveChangesAsync();
                return f;
            }
            catch (Exception e)
            {
                throw (e);
            }
            
        }
        public async Task<Friends> RemoveFriend(Friends f)
        {
            try
            {
                _context.Friends.Remove(f);
                await _context.SaveChangesAsync();
                return f;
            }
            catch (Exception e)
            {
                throw (e);
            }
            
        }
        public bool FriendExist(Friends f)
        {
            return _context.Friends.Any(x => x.UserId == f.UserId && x.FriendId == f.FriendId);
        }
    }
}
