using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JellBreak.Models;
using MongoDB.Driver;
using System.Collections;

namespace JellBreak.Utils
{
        public class BoardUtils
        {
            private readonly IMongoCollection<BoardModel> _boardCollection;
            private readonly IMongoCollection<ListModel> _listCollection;
            private readonly IMongoCollection<CardModel> _cardCollection;
            private readonly IMongoCollection<TaskModel> _subtaskCollection;

            public BoardUtils(IMongoDatabase database)
            {
                _boardCollection = database.GetCollection<BoardModel>("Boards");
                _listCollection = database.GetCollection<ListModel>("Lists");
                _cardCollection = database.GetCollection<CardModel>("Cards");
                _subtaskCollection = database.GetCollection<TaskModel>("Subtasks");
            }

            public async Task PopulateGuestBoardAsync(string userId)
            {
                var boardId = Guid.NewGuid().ToString(); // You can use a unique identifier logic here

                var guestBoard = new BoardModel
                {
                    Id = boardId,
                    CreatorId = userId,
                    BoardTitle = "Intro Board",
                };

                var guestLists = new List<ListModel>
            {
                new ListModel
                {
                    Id = Guid.NewGuid().ToString(),
                    ListTitle = "Backlog",
                    Position = 16384,
                    CoverColor = "gray",
                    BoardId = boardId,
                },
                // ... (add other lists)
            };

                var guestCards = new List<CardModel>
            {
                new CardModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CardTitle = "Welcome to JellBreak!",
                    Description = "JellBreak is a powerful tool to visually manage your workflow.",
                    Priority = "low",
                    Position = 16384,
                    ListId = guestLists[0].Id,
                    BoardId = boardId,
                },
                // ... (add other cards)
            };

                var guestSubtasks = new List<TaskModel>
            {
                new TaskModel
                {
                    TaskName = "Add a new list",
                    IsDone = true,
                    Position = 16384,
                    CardId = guestCards[1].Id,
                    BoardId = boardId,
                },
                // ... (add other subtasks)
            };

                try
                {
                    await _boardCollection.InsertOneAsync(guestBoard);
                    await _listCollection.InsertManyAsync(guestLists);
                    await _cardCollection.InsertManyAsync(guestCards);
                    await _subtaskCollection.InsertManyAsync(guestSubtasks);
                }
                catch (Exception ex)
                {
                    // Handle the exception as needed
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }

}
