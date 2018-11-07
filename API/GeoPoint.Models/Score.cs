using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GeoPoint.Models
{
    public class Score
    {
        [BsonId]
        [Required]
        public ObjectId Id { get; set; }
        [BsonElement("Value")]
        public int Value { get; set; }
        [BsonElement("Area")]
        public string Area { get; set; }
        [BsonElement("TimeSpan")]
        public double TimeSpan { get; set; }
        [BsonElement("TimeStamp")]
        public DateTime TimeStamp { get; set; }
        [BsonElement("UserId")]
        public string UserId { get; set; }

        
    }
}
