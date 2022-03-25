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
    public class BuchController : Controller
    {
        private readonly MyDBContext _context;

        public BuchController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Buch
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buch.ToListAsync());
        }

        // GET: Buch/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buch = await _context.Buch
                .FirstOrDefaultAsync(m => m.Buchnummer == id);
            if (buch == null)
            {
                return NotFound();
            }

            return View(buch);
        }

        // GET: Buch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Buchnummer,Sachgebiet,Titel,Autor,Ort,Erscheinungsjahr")] Buch buch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buch);
        }

        // GET: Buch/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buch = await _context.Buch.FindAsync(id);
            if (buch == null)
            {
                return NotFound();
            }
            return View(buch);
        }

        // POST: Buch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Buchnummer,Sachgebiet,Titel,Autor,Ort,Erscheinungsjahr")] Buch buch)
        {
            if (id != buch.Buchnummer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuchExists(buch.Buchnummer))
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
            return View(buch);
        }

        // GET: Buch/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buch = await _context.Buch
                .FirstOrDefaultAsync(m => m.Buchnummer == id);
            if (buch == null)
            {
                return NotFound();
            }

            return View(buch);
        }

        // POST: Buch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var buch = await _context.Buch.FindAsync(id);
            _context.Buch.Remove(buch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuchExists(string id)
        {
            return _context.Buch.Any(e => e.Buchnummer == id);
        }
    }
}
