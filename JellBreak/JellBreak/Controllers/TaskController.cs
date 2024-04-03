using JellBreak.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace JellBreak.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly MongoDBDataAccess _mongoDBDataAccess;

        public TaskController()
        {
            var databaseName = "test";

            _mongoDBDataAccess = new MongoDBDataAccess(databaseName);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskModel>> GetAllTasks()
        {
            try
            {
                var collection = _mongoDBDataAccess.GetTaskCollection();
                var tasks = collection.Find(new BsonDocument()).ToList();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving task: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }

}
