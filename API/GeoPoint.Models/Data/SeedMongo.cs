using GeoPoint.Models.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoPoint.Models.Data
{
    public class SeedMongo
    {
        private readonly IScoreRepo scoreRepo;
        private readonly IConfiguration configuration;
        private readonly GeoPointAPIMongoDBContext mongoDBContext;
        public SeedMongo(IScoreRepo scoreRepo, IConfiguration configuration, GeoPointAPIMongoDBContext mongoDBContext)
        {
            this.scoreRepo = scoreRepo;
            this.configuration = configuration;
            this.mongoDBContext = mongoDBContext;

        }
        public void initDatabase(int nmbrScores)
        {
            if (!mongoDBContext.CollectionExistsAsync("scores").Result)
            {
                List<string> AreaList = new List<string> { "EU", "AF", "SA", "NA" };
                List<string> UserList = new List<string> { "3f316e83-896f-47c3-95c7-24e70d25afa9", "18d74f91-0417-4f61-9809-68e2ca21ba5f", "763f45a5-0ec3-413b-ad02-6683f9dc8e40" };
                for (var i = 0; i < nmbrScores; i++)
                {
                    scoreRepo.CreateAsync(new Models.MongoModels.Score
                    {
                        Value = new Random().Next(30),
                        Area = AreaList[new Random().Next(AreaList.Count)],
                        UserId = UserList[new Random().Next(UserList.Count)],
                        TimeSpan = new Random().Next(30)
                    });
                }
            }
        }
    }
}
