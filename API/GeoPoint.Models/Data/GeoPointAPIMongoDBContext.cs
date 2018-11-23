using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Data
{
    public class GeoPointAPIMongoDBContext
    {
        private readonly IConfiguration configuration;
        public readonly IMongoDatabase Database;

        public GeoPointAPIMongoDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            MongoClient client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            Database = client.GetDatabase(configuration.GetSection("MongoDatabases")["GeoPoint"]);
        }
        public IMongoCollection<Score> Scores => Database.GetCollection<Score>("scores");
        public IMongoCollection<GeoPointUser> Users => Database.GetCollection<GeoPointUser>("Users");
       

        public async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = await Database.ListCollectionsAsync(new ListCollectionsOptions { Filter = FilterDefinition<BsonDocument>.Empty });
            return await collections.AnyAsync();
        }
    }
}
