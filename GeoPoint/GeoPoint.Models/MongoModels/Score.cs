using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoPoint.Models.MongoModels
{
    [BsonIgnoreExtraElements]
    public class Score
    {
        public Score()
        {

        }
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Value")]
        public int Value { get; set; }
        [BsonElement("Area")]
        public string Area { get; set; }
        [BsonElement("TimeSpan")]
        public double TimeSpan { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }
    }
}
