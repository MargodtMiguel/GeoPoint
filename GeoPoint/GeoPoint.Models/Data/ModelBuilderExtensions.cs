using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.Models.Data
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var users = new[]
            {
                new tblUsers {Id = Guid.NewGuid().ToString(), UserName = "RuneClaeys", PasswordHash = "rune", IsAdmin = true},
                new tblUsers {Id = Guid.NewGuid().ToString(), UserName = "MiguelMargodt", PasswordHash = "miguel", IsAdmin = true },
                new tblUsers {Id = Guid.NewGuid().ToString(), UserName = "BramVuylseke", PasswordHash = "bram", IsAdmin = false }

            };
            modelBuilder.Entity<tblUsers>().HasData(users);

            var scores = new[] {
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Score = 8, TimeStamp = DateTime.Now, Map = "EU"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Score = 4, TimeStamp = DateTime.Now, Map = "EU"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Score = 10, TimeStamp = DateTime.Now, Map = "EU"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Score = 5, TimeStamp = DateTime.Now, Map = "NA"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Score = 14, TimeStamp = DateTime.Now, Map = "NA"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Score = 8, TimeStamp = DateTime.Now, Map = "NA"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Score = 7, TimeStamp = DateTime.Now, Map = "AF"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Score = 6, TimeStamp = DateTime.Now, Map = "AF"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Score = 6, TimeStamp = DateTime.Now, Map = "AF"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Score = 3, TimeStamp = DateTime.Now, Map = "ZA"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Score = 5, TimeStamp = DateTime.Now, Map = "ZA"},
                    new tblScores {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Score = 7, TimeStamp = DateTime.Now, Map = "ZA"}
                };
            modelBuilder.Entity<tblScores>().HasData(scores);

            var friends = new[] {
                    new tblFriends {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[1].Id, isPending = false},
                    new tblFriends {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[2].Id, isPending = true},
                    new tblFriends {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, FriendId = users[0].Id, isPending = false},
                    new tblFriends {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, FriendId = users[2].Id, isPending = true},
            };
            modelBuilder.Entity<tblFriends>().HasData(friends);
        }
    }
}
