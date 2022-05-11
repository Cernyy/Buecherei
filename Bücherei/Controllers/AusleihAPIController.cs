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
    public class AusleihAPIController : ControllerBase
    {
        private readonly MyDBContext _context;

        public AusleihAPIController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/AusleihAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ausleihe>>> GetAusleihe()
        {
            return await _context.Ausleihe.ToListAsync();
        }

        // GET: api/AusleihAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ausleihe>> GetAusleihe(int id)
        {
            var ausleihe = await _context.Ausleihe.FindAsync(id);

            if (ausleihe == null)
            {
                return NotFound();
            }

            return ausleihe;
        }

        // PUT: api/AusleihAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAusleihe(int id, Ausleihe ausleihe)
        {
            if (id != ausleihe.Id)
            {
                return BadRequest();
            }

            _context.Entry(ausleihe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AusleiheExists(id))
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

        // POST: api/AusleihAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ausleihe>> PostAusleihe(Ausleihe ausleihe)
        {
            _context.Ausleihe.Add(ausleihe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAusleihe", new { id = ausleihe.Id }, ausleihe);
        }

        // DELETE: api/AusleihAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAusleihe(int id)
        {
            var ausleihe = await _context.Ausleihe.FindAsync(id);
            if (ausleihe == null)
            {
                return NotFound();
            }

            _context.Ausleihe.Remove(ausleihe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AusleiheExists(int id)
        {
            return _context.Ausleihe.Any(e => e.Id == id);
        }
    }
}
