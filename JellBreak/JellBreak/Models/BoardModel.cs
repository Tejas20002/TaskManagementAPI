using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JellBreak.Models
{
    public class BoardModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("creatorId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatorId { get; set; }

        [BsonElement("boardTitle")]
        public string BoardTitle { get; set; }

        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        public string Url => $"/b/{Id}";
    }

}
