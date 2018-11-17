using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoPoint.Models
{
    public class CurUsers
    {
        [BsonId]
        [Required]
        public ObjectId Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("ConnectionID")]
        public string ConnectionID { get; set; }
    }
}
