using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bücherei.Data;
using Bücherei.Models;

namespace Bücherei.Controllers
{
    public class AusleiheController : Controller
    {
        private readonly MyDBContext _context;

        public AusleiheController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Ausleihe
        public async Task<IActionResult> Index()
        {
            var myDBContext = _context.Ausleihe.Include(a => a.Buch).Include(a => a.SchuelerIn);
            return View(await myDBContext.ToListAsync());
        }

        // GET: Ausleihe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausleihe = await _context.Ausleihe
                .Include(a => a.Buch)
                .Include(a => a.SchuelerIn)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ausleihe == null)
            {
                return NotFound();
            }

            return View(ausleihe);
        }

        // GET: Ausleihe/Create
        public IActionResult Create()
        {
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer");
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname");
            return View();
        }

        // POST: Ausleihe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Buchnummer,Ausweisnummer,Ausleihdatum,Retourdatum")] Ausleihe ausleihe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ausleihe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
            return View(ausleihe); 
        }
        // GET: Ausleihe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausleihe = await _context.Ausleihe.FindAsync(id);
            if (ausleihe == null)
            {
                return NotFound();
            }
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
            return View(ausleihe);
        }

        // POST: Ausleihe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Buchnummer,Ausweisnummer,Ausleihdatum,Retourdatum")] Ausleihe ausleihe)
        {
            if (id != ausleihe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ausleihe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AusleiheExists(ausleihe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Buchnummer"] = new SelectList(_context.Buch, "Buchnummer", "Buchnummer", ausleihe.Buchnummer);
            ViewData["Ausweisnummer"] = new SelectList(_context.SchuelerIn, "Ausweisnummer", "Nachname", ausleihe.Ausweisnummer);
            return View(ausleihe);
        }

        // GET: Ausleihe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ausleihe = await _context.Ausleihe
                .Include(a => a.Buch)
                .Include(a => a.SchuelerIn)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ausleihe == null)
            {
                return NotFound();
            }

            return View(ausleihe);
        }

        // POST: Ausleihe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ausleihe = await _context.Ausleihe.FindAsync(id);
            _context.Ausleihe.Remove(ausleihe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AusleiheExists(int id)
        {
            return _context.Ausleihe.Any(e => e.Id == id);
        }

        [HttpPost]
        public JsonResult GetBuch(string Buchnummer)
        {
            var buch = _context.Buch.Where(b => b.Buchnummer == Buchnummer).FirstOrDefault();
            return Json(buch);
        }

        [HttpPost]
        public JsonResult GetSchuelerIn(string Ausweisnummer)
        {
            var schuelerIn = _context.SchuelerIn.Where(s => s.Ausweisnummer.ToString() == Ausweisnummer).FirstOrDefault();
            return Json(schuelerIn);
        }

        [HttpPost]
        public void InsertListe([FromBody] List<Buch> buch)
        {
            //using (_context)
            //{
            //    if (buchList == null)
            //    {
            //        buchList = new List<Buch>();
            //    }
            //    foreach (Buch buch in buchList)
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            _context.Buch.Add(buch);
            //            _context.SaveChangesAsync();
            //        }
            //    }
            //}

        }
    }
}
