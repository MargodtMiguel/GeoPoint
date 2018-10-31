using GeoPoint.Models;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Data
{
    public class SeedIdentity
    {
        private readonly UserManager<GeoPointUser> userMgr;
        private readonly RoleManager<IdentityRole> roleMgr;
        private readonly IScoreRepo _scoreRepo;
        private readonly IFriendsRepo _friendsRepo;

        public SeedIdentity(UserManager<GeoPointUser> userMgr, RoleManager<IdentityRole> roleMgr, IScoreRepo scoreRepo, IFriendsRepo friendsRepo)
        {
            this.userMgr = userMgr;
            this.roleMgr = roleMgr;
            _friendsRepo = friendsRepo;
            _scoreRepo = scoreRepo;
        }

        public async Task SeedIdentityMobileAppsAPI()
        {
            var users = new[]
            {
                new GeoPointUser {Id = "3f316e83-896f-47c3-95c7-24e70d25afa9", SecurityStamp = Guid.NewGuid().ToString(), UserName = "RuneClaeys" },
                new GeoPointUser {Id = "18d74f91-0417-4f61-9809-68e2ca21ba5f", SecurityStamp = Guid.NewGuid().ToString(), UserName = "MiguelMargodt" },
                new GeoPointUser {Id = "763f45a5-0ec3-413b-ad02-6683f9dc8e40", SecurityStamp = Guid.NewGuid().ToString(), UserName = "JohanVannieuwenhuyse" },

            };
            foreach (var u in users)
            {
                var user = await userMgr.FindByNameAsync(u.UserName);
                Debug.WriteLine(user);
                if(user == null)
                {
                    if (!(await roleMgr.RoleExistsAsync("Admin")))
                    {
                        var role = new IdentityRole("Admin");
                        await roleMgr.CreateAsync(role);

                    }
                    var userResult = await userMgr.CreateAsync(u, "P@ssw0rd");
                    var roleResult = await userMgr.AddToRoleAsync(u, "Admin");
                    if (!userResult.Succeeded || !roleResult.Succeeded)
                    {
                        throw new InvalidOperationException("Failed to build user and roles");
                    }
                }
            }
            //Seeding Friends
            var friends = new[] {
                new Friends { Id = Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[1].Id, isPending = false},
                new Friends { Id = Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[2].Id },
                new Friends { Id = Guid.NewGuid().ToString(), UserId = users[1].Id, FriendId = users[2].Id, isPending = false },
            };
            
            foreach (Friends f in friends)
            {
                if (!(_friendsRepo.FriendExist(f)))
                {
                    await _friendsRepo.SendFriendRequest(f);
                }
            }

            //Seeding Scores
            var scores = new[]
            {
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value=10,TimeStamp = DateTime.Now, Area = "EU" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value=8,TimeStamp = DateTime.Now, Area = "NA" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value=4,TimeStamp = DateTime.Now, Area = "SA" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value=11,TimeStamp = DateTime.Now, Area = "AF" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value=14,TimeStamp = DateTime.Now, Area = "EU" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value=6,TimeStamp = DateTime.Now, Area = "NA" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value=7,TimeStamp = DateTime.Now, Area = "SA" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value=1,TimeStamp = DateTime.Now, Area = "AF" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value=8,TimeStamp = DateTime.Now, Area = "EU" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value=8,TimeStamp = DateTime.Now, Area = "NA" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value=11,TimeStamp = DateTime.Now, Area = "SA" },
                new Score{ Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value=11,TimeStamp = DateTime.Now, Area = "AF" }
            };

            foreach(Score s in scores)
            {
                if (!(_scoreRepo.ScoreExists(s)))
                {
                    await _scoreRepo.CreateScore(s);
                }
            }
        }
    }
}
