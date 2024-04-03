using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JellBreak.Models
{
    public class CardModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("cardTitle")]
        public string CardTitle { get; set; }

        [BsonElement("coverColor")]
        public string CoverColor { get; set; }

        [BsonElement("priority")]
        public string Priority { get; set; }

        [BsonElement("position")]
        public int Position { get; set; }

        [BsonElement("listId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ListId { get; set; }

        [BsonElement("boardId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BoardId { get; set; }

        [BsonElement("createdAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }

}
