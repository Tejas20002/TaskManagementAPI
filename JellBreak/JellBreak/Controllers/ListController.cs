using Microsoft.AspNetCore.Mvc;
using JellBreak.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JellBreak.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly MongoDBDataAccess _mongoDBDataAccess;

        public ListController()
        {
            var databaseName = "test";

            _mongoDBDataAccess = new MongoDBDataAccess (databaseName);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ListModel>> GetAllLists()
        {
            try { 
                var collection = _mongoDBDataAccess.GetListCollection();
                var lists = collection.Find(new BsonDocument()).ToList();
                return Ok(lists);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving Lists: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}
