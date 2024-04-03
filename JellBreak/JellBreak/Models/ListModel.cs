using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JellBreak.Models
{
    public class ListModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("listTitle")]
        public string ListTitle { get; set; }

        [BsonElement("position")]
        public int Position { get; set; }

        [BsonElement("coverColor")]
        public string CoverColor { get; set; }

        [BsonElement("boardId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BoardId { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }

}
