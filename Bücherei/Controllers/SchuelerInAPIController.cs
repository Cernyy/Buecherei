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
    public class SchuelerInAPIController : ControllerBase
    {
        private readonly MyDBContext _context;

        public SchuelerInAPIController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/SchuelerInAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchuelerIn>>> GetSchuelerIn()
        {
            return await _context.SchuelerIn.ToListAsync();
        }

        // GET: api/SchuelerInAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchuelerIn>> GetSchuelerIn(int id)
        {
            var schuelerIn = await _context.SchuelerIn.FindAsync(id);

            if (schuelerIn == null)
            {
                return NotFound();
            }

            return schuelerIn;
        }

        // PUT: api/SchuelerInAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchuelerIn(int id, SchuelerIn schuelerIn)
        {
            if (id != schuelerIn.Ausweisnummer && HasNoFKConnections(id))
            {
                //alten eintrag löschen und neuen erstellen
                await DeleteSchuelerIn(id);
                await PostSchuelerIn(schuelerIn);
                return NoContent();
            }
            _context.Entry(schuelerIn).State = EntityState.Modified;

            try
            {
                _context.SchuelerIn.Update(schuelerIn);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchuelerInExists(id))
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

        // POST: api/SchuelerInAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchuelerIn>> PostSchuelerIn(SchuelerIn schuelerIn)
        {
            _context.SchuelerIn.Add(schuelerIn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchuelerIn", new { id = schuelerIn.Ausweisnummer }, schuelerIn);
        }

        // DELETE: api/SchuelerInAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchuelerIn(int id)
        {
            var schuelerIn = await _context.SchuelerIn.FindAsync(id);
            if (schuelerIn == null)
            {
                return NotFound();
            }

            _context.SchuelerIn.Remove(schuelerIn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchuelerInExists(int id)
        {
            return _context.SchuelerIn.Any(e => e.Ausweisnummer == id);
        }

        private bool HasNoFKConnections(int id)
        {
            Ausleihe test = _context.Ausleihe.Where<Ausleihe>(a => a.Ausweisnummer == id).FirstOrDefault();
            if (test != null)
                return false;
            return true;
        }
    }
}
