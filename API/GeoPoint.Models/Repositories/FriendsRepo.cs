using GeoPoint.API.ViewModels;
using GeoPoint.Models.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<PublicProfileVM>> GetFriendsByUserId(string UserId)
        {
            try
            {
                IEnumerable<Friends> friends = await _context.Friends.Where(x =>( (x.UserId == UserId || x.FriendId == UserId) && x.isPending == false )).Include("Friend").Include("User").AsNoTracking().ToListAsync();
                List<PublicProfileVM> pps = new List<PublicProfileVM>();
                foreach (Friends f in friends)
                {
                    if(!(f.UserId == UserId))
                    {
                        f.Friend = f.User;
                    }
                    f.User = null;
                    PublicProfileVM pp = new PublicProfileVM { ProfileId = f.Friend.Id,UserName = f.Friend.UserName };
                    pps.Add(pp);
                }
               

                return pps;
            }
            catch (Exception e)
            {
                throw (e);
            }
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
        public async Task<bool> ConfirmFriends(Friends f)
        {
            try
            {
                f = _context.Friends.Where(x => x.Id == f.Id).SingleOrDefault();
                f.isPending = false;
                _context.Friends.Update(f);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public async Task<bool> RemoveFriend(Friends f)
        {
            try
            {
                _context.Friends.Remove(f);
                await _context.SaveChangesAsync();
                Friends f2 = f;
                f2.UserId = f.FriendId;
                f2.FriendId = f.UserId;
                _context.Friends.Remove(f2);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public bool FriendExist(Friends f)
        {
            return _context.Friends.Any(x => x.UserId == f.UserId && x.FriendId == f.FriendId);
        }
    }
}
