using JellBreak.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JellBreak.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly MongoDBDataAccess _mongoDBDataAccess;

        public CardController()
        {
            var databaseName = "test";

            _mongoDBDataAccess = new MongoDBDataAccess(databaseName);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CardModel>> GetAllCards()
        {
            try
            {
                var collection = _mongoDBDataAccess.GetCardCollection();
                var cards = collection.Find(new BsonDocument()).ToList();
                return Ok(cards);
            }
            catch (Exception ex)    
            {
                Console.WriteLine($"Error retrieving cards: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}
