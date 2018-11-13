using AspNetCore.Identity.Mongo.Model;
using GeoPoint.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Data
{
    public class SeedMongo
    {
        private readonly IConfiguration configuration;
        private readonly GeoPointAPIMongoDBContext mongoDBContext;
        private readonly UserManager<GeoPointUser> userMgr;
        private readonly RoleManager<MongoRole> roleMgr;
        private readonly IScoreRepo scoreRepo;
        
        public SeedMongo(UserManager<GeoPointUser> userMgr, RoleManager<MongoRole> roleMgr, IScoreRepo scoreRepo, IConfiguration configuration, GeoPointAPIMongoDBContext mongoDBContext)
        {
            this.scoreRepo = scoreRepo;
            this.configuration = configuration;
            this.mongoDBContext = mongoDBContext;
            this.userMgr = userMgr;
            this.roleMgr = roleMgr;

        }
        public async Task initDatabase(int nmbrScores)
        {
            try
            {
                var users = new[]
           {
                new GeoPointUser { SecurityStamp = Guid.NewGuid().ToString(), UserName = "RuneClaeys" },
                new GeoPointUser { SecurityStamp = Guid.NewGuid().ToString(), UserName = "MiguelMargodt" },
                new GeoPointUser { SecurityStamp = Guid.NewGuid().ToString(), UserName = "JohanVannieuwenhuyse" },

            };
                foreach (var u in users)
                {
                    var user = await userMgr.FindByNameAsync(u.UserName);
                    Debug.WriteLine(user);
                    if (user == null)
                    {
                        if (!(await roleMgr.RoleExistsAsync("Admin")))
                        {
                            var role = new MongoRole("Admin");
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
                if (!mongoDBContext.CollectionExistsAsync("scores").Result)
                {
                    List<string> AreaList = new List<string> { "EUROPE", "AFRICA", "SOUTH-AMERICA", "NORTH-AMERICA", "AUSTRALIA" };
                    for (var i = 0; i < nmbrScores; i++)
                    {
                        DateTime date = DateTime.UtcNow.AddDays(-new Random().Next(7));
                        date = date.AddMinutes(-new Random().Next(60));
                        date = date.AddHours(-new Random().Next(24));
                        date = date.AddSeconds(-new Random().Next(60));
                        Debug.WriteLine(date.ToString());

                        await scoreRepo.CreateAsync(new Score
                        {
                            Value = new Random().Next(30),
                            Area = AreaList[new Random().Next(AreaList.Count)],
                            User = users[new Random().Next(users.Length)],
                            TimeSpan = new Random().Next(30),
                            TimeStamp = date
                        });
                    }
                }
            }
            catch(Exception e)
            {

            }
        }
    }
}
