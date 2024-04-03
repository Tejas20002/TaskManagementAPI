using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JellBreak.Models
{
    public class TaskModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("taskName")]
        public string TaskName { get; set; }

        [BsonElement("isDone")]
        public bool IsDone { get; set; }

        [BsonElement("position")]
        public int Position { get; set; }

        [BsonElement("cardId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CardId { get; set; }

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
    }

}
