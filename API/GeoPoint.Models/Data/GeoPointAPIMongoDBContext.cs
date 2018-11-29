using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace GeoPoint.Models.Data
{
    public class GeoPointAPIMongoDBContext
    {
        public readonly IMongoDatabase Database;

        public GeoPointAPIMongoDBContext()
        {
            string connectionString =
  @"mongodb://geopoint:cVP4AZwCGVxRjaMEqdNHBTBFHWEa4z1M3Is2MK9APpKX5EeoOjpIlvrfz48IA6IofQbey922M8dsDyGS3ngtEA==@geopoint.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings =
              new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            Database = mongoClient.GetDatabase("GeoPoint");
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
