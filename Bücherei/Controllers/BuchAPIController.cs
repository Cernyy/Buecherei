using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bücherei.Data;
using Bücherei.Models;

namespace Bücherei.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuchAPIController : ControllerBase
    {
        private readonly MyDBContext _context;

        public BuchAPIController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/BuchAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buch>>> GetBuch()
        {
            return await _context.Buch.ToListAsync();
        }

        // GET: api/BuchAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buch>> GetBuch(string id)
        {
            var buch = await _context.Buch.FindAsync(id);

            if (buch == null)
            {
                return NotFound();
            }

            return buch;
        }

        // PUT: api/BuchAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuch(string id, Buch buch)
        {
            if (id != buch.Buchnummer)
            {
                return BadRequest();
            }

            _context.Entry(buch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuchExists(id))
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

        // POST: api/BuchAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buch>> PostBuch(Buch buch)
        {
            _context.Buch.Add(buch);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BuchExists(buch.Buchnummer))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBuch", new { id = buch.Buchnummer }, buch);
        }

        // DELETE: api/BuchAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuch(string id)
        {
            var buch = await _context.Buch.FindAsync(id);
            if (buch == null)
            {
                return NotFound();
            }

            _context.Buch.Remove(buch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuchExists(string id)
        {
            return _context.Buch.Any(e => e.Buchnummer == id);
        }
    }
}
