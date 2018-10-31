using Microsoft.AspNetCore.Identity;
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
                new GeoPointUser{Id = "3f316e83-896f-47c3-95c7-24e70d25afa9"},
                new GeoPointUser{Id = "763f45a5-0ec3-413b-ad02-6683f9dc8e40"},
                new GeoPointUser{Id = "18d74f91-0417-4f61-9809-68e2ca21ba5f"}
            };
            var Scores = new[]
            {
                //User: RuneClaeys
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value = 10, TimeStamp = DateTime.Now, Area= "EU" },
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value = 5, TimeStamp = DateTime.Now, Area= "NA" },
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[0].Id, Value = 15, TimeStamp = DateTime.Now, Area= "SA" },
                //User: JohanVannieuwenhuyse
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value = 4, TimeStamp = DateTime.Now, Area= "EU" },
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value = 11, TimeStamp = DateTime.Now, Area= "NA" },
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[1].Id, Value = 5, TimeStamp = DateTime.Now, Area= "SA" },
                //User: MiguelMargodt
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value = 13, TimeStamp = DateTime.Now, Area= "EU" },
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value = 8, TimeStamp = DateTime.Now, Area= "NA" },
                new Score {Id = Guid.NewGuid().ToString(), UserId = users[2].Id, Value = 5, TimeStamp = DateTime.Now, Area= "SA" }

            };
            modelBuilder.Entity<Score>().HasData(Scores);

            var Friends = new[]
            {
                //Friends: RuneClaeys
                new Friends {Id= Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[2].Id, isPending=false },
                new Friends {Id= Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[1].Id, isPending=true },

                //Friends: JohanVannuewenhuyse
                new Friends {Id= Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[0].Id, isPending = true },
                new Friends {Id= Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[2].Id, isPending = false },

                //Friends: MiguelMargodt
                new Friends {Id= Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[0].Id, isPending = false },
                new Friends {Id= Guid.NewGuid().ToString(), UserId = users[0].Id, FriendId = users[1].Id, isPending = false },

            };
            modelBuilder.Entity<Friends>().HasData(Friends);



   
        }
    }
}
