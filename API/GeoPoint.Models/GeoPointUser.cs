using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeoPoint.Models
{
    public class GeoPointUser : MongoUser
    {
        [BsonElement("ConnectionId")]
        public string ConnectionId { get; set; } = "";
        [BsonIgnoreIfNull]
        public IList<Friend> Friends { get; set; }
    }
    public class Friend
    {
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("IsPending")]
        public bool IsPending { get; set; } = true;
    }
    
}
