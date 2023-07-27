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
    [Route("api/Cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CardsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(CardCreateDto cardCreateDto)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'ApiDbContext.Cards' is null.");
            }
            if (_context.Swimlanes == null)
            {
                return Problem("Entity set 'ApiDbContext.Swimlanes'  is null.");
            }
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApiDbContext.Users'  is null.");
            }

            var swimlane = await _context.Swimlanes.FindAsync(cardCreateDto.SwimlaneId);
            var user = await _context.Users.FindAsync(cardCreateDto.UserId);

            if (swimlane == null)
            {
                return NotFound();
            }
            if (user == null)
            {
                return NotFound();
            }

            Card card =
                new()
                {
                    Title = cardCreateDto.Title,
                    Body = cardCreateDto.Body,
                    Swimlane = swimlane,
                    User = user
                };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            if (_context.Cards == null)
            {
                return NotFound();
            }
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return (_context.Cards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
