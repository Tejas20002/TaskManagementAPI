using JellBreak.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JellBreak
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<BoardModel> Boards => _database.GetCollection<BoardModel>("boards");
        public IMongoCollection<ListModel> Lists => _database.GetCollection<ListModel>("lists");
        // Add similar properties for other collections
    }
    public class MongoDBDataAccess
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public string connectionUri = "mongodb+srv://thirani969:Apstndp20@scatum.dbbnub6.mongodb.net/?retryWrites=true&w=majority";


        public MongoDBDataAccess(string databaseName)
        {
            _client = new MongoClient(connectionUri);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoCollection<UserModel> GetUserCollection()
        {
            return _database.GetCollection<UserModel>("accounts");
        }

        public IMongoCollection<BoardModel> GetBoardCollection()
        {
            return _database.GetCollection<BoardModel>("boards");
        }

        public IMongoCollection<ListModel> GetListCollection()
        {
            return _database.GetCollection<ListModel>("lists");
        }

        public IMongoCollection<CardModel> GetCardCollection()
        {
            return _database.GetCollection<CardModel>("cards");
        }

        public IMongoCollection<TaskModel> GetTaskCollection()
        {
            return _database.GetCollection<TaskModel>("subtasks");
        }
    }

}
