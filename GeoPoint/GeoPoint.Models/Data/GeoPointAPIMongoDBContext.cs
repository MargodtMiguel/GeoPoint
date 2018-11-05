using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoPoint.Models.Data
{
    public class GeoPointAPIMongoDBContext
    {
        private readonly IConfiguration configuration;
        private readonly IMongoDatabase Database;

        public GeoPointAPIMongoDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            MongoClient client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            Database = client.GetDatabase(configuration.GetSection("MongoDatabases")["GeoPoint"]);

        }
        public IMongoCollection<MongoModels.Score> Scores => Database.GetCollection<MongoModels.Score>("Scores");
    }
}
