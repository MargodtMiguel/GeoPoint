using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeoPoint.API.ViewModels
{
    public class ScoreUserVM
    {
        [BsonId]
        [Required]
        public ObjectId Id { get; set; }
        [BsonElement("username")]
        public string Username { get; set; }
    }
}
