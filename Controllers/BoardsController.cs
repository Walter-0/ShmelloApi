using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShmelloApi.Data;
using ShmelloApi.DTOs;
using ShmelloApi.Models;

namespace ShmelloApi.Controllers
{
    [Route("api/Boards")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public BoardsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            return await _context.Boards.Include(b => b.Swimlanes).ToListAsync();
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            _context.Entry(board).Collection(b => b.Swimlanes).Load();

            foreach (var swimlane in board.Swimlanes)
            {
                _context.Entry(swimlane).Collection(s => s.Cards).Load();
            }

            return board;
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Boards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(BoardCreateDto boardCreateDto)
        {
            if (_context.Boards == null)
            {
                return Problem("Entity set 'ApiDbContext.Boards' is null.");
            }

            var user = await _context.Users.FindAsync(boardCreateDto.UserId);

            if (user == null)
            {
                return NotFound();
            }

            Board board = new() { Title = boardCreateDto.Title, User = user };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(int id)
        {
            return (_context.Boards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
