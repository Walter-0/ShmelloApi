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
    [Route("api/Swimlanes")]
    [ApiController]
    public class SwimlanesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public SwimlanesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Swimlanes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Swimlane>>> GetSwimlanes()
        {
            if (_context.Swimlanes == null)
            {
                return NotFound();
            }
            return await _context.Swimlanes.ToListAsync();
        }

        // GET: api/Swimlanes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Swimlane>> GetSwimlane(int id)
        {
            if (_context.Swimlanes == null)
            {
                return NotFound();
            }
            var swimlane = await _context.Swimlanes.FindAsync(id);

            if (swimlane == null)
            {
                return NotFound();
            }

            return swimlane;
        }

        // PUT: api/Swimlanes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSwimlane(int id, Swimlane swimlane)
        {
            if (id != swimlane.Id)
            {
                return BadRequest();
            }

            _context.Entry(swimlane).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SwimlaneExists(id))
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

        // POST: api/Swimlanes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Swimlane>> PostSwimlane(SwimlaneCreateDto swimlaneCreateDto)
        {
            if (_context.Swimlanes == null)
            {
                return Problem("Entity set 'ApiDbContext.Swimlanes'  is null.");
            }
            if (_context.Boards == null)
            {
                return Problem("Entity set 'ApiDbContext.Boards'  is null.");
            }

            Board board = _context.Boards.Single(b => b.Id == swimlaneCreateDto.BoardId);
            Swimlane swimlane = new() { Title = swimlaneCreateDto.Title, Board = board };

            _context.Swimlanes.Add(swimlane);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSwimlane), new { id = swimlane.Id }, swimlane);
        }

        // DELETE: api/Swimlanes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSwimlane(int id)
        {
            if (_context.Swimlanes == null)
            {
                return NotFound();
            }
            var swimlane = await _context.Swimlanes.FindAsync(id);
            if (swimlane == null)
            {
                return NotFound();
            }

            _context.Swimlanes.Remove(swimlane);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SwimlaneExists(int id)
        {
            return (_context.Swimlanes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
