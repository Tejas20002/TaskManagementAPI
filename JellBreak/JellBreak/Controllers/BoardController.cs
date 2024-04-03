using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using JellBreak.Models;

namespace JellBreak.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly UserManager<UserModel> _userManager; // Replace with your actual user manager
        private readonly MongoDBDataAccess _dbContext; // Replace with your actual DbContext

        public BoardController(ILogger<BoardController> logger, UserManager<UserModel> userManager, MongoDBDataAccess dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("user-boards")]
        public async Task<IActionResult> GetUserBoards()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var boards = _dbContext.Board.Where(b => b.CreatorId == userId).ToList();
                return Ok(boards);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("board/{id}")]
        public async Task<IActionResult> GetBoard(string id)
        {
            try
            {
                var boardId = Guid.Parse(id);
                var userId = _userManager.GetUserId(User);

                var board = _dbContext.Board
                    .Where(b => b.Id == boardId && b.CreatorId == userId)
                    .Select(b => new
                    {
                        b.Id,
                        b.BoardTitle,
                        Lists = _dbContext.Lists
                            .Where(l => l.BoardId == boardId)
                            .Select(l => new
                            {
                                l.Id,
                                l.ListTitle,
                                Cards = _dbContext.Cards
                                    .Where(c => c.ListId == l.Id)
                                    .Select(c => new
                                    {
                                        c.Id,
                                        c.CardTitle,
                                        Subtasks = _dbContext.Subtasks
                                            .Where(s => s.CardId == c.Id)
                                            .Select(s => new
                                            {
                                                s.TaskName,
                                                s.IsDone,
                                                s.Position
                                            })
                                            .ToList()
                                    })
                                    .ToList()
                            })
                            .ToList()
                    })
                    .FirstOrDefault();

                if (board == null)
                {
                    return NotFound();
                }

                return Ok(board);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("create-board")]
        public async Task<IActionResult> CreateBoard([FromBody] UserModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = _userManager.GetUserId(User);

                var newBoard = new Board
                {
                    CreatorId = userId,
                    BoardTitle = request.BoardTitle
                };

                _dbContext.Boards.Add(newBoard);
                await _dbContext.SaveChangesAsync();

                return Ok(newBoard);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete-board/{id}")]
        public async Task<IActionResult> DeleteBoard(string id)
        {
            try
            {
                var boardId = Guid.Parse(id);
                var userId = _userManager.GetUserId(User);

                var boardToDelete = _dbContext.Boards.FirstOrDefault(b => b.Id == boardId && b.CreatorId == userId);

                if (boardToDelete == null)
                {
                    return NotFound();
                }

                _dbContext.Boards.Remove(boardToDelete);
                await _dbContext.SaveChangesAsync();

                // Delete related lists, cards, and subtasks here

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch("update-board/{id}")]
        public async Task<IActionResult> UpdateBoard(string id, [FromBody] UpdateBoardRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var boardId = Guid.Parse(id);
                var userId = _userManager.GetUserId(User);

                var boardToUpdate = _dbContext.Boards.FirstOrDefault(b => b.Id == boardId && b.CreatorId == userId);

                if (boardToUpdate == null)
                {
                    return NotFound();
                }

                boardToUpdate.BoardTitle = request.BoardTitle;

                _dbContext.Boards.Update(boardToUpdate);
                await _dbContext.SaveChangesAsync();

                return Ok(boardToUpdate);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

}

