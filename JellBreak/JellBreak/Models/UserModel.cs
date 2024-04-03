using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JellBreak.Models
{ 
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public string Url => $"/{Username}";
    }

}
