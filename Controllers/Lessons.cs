using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Controllers
{
    public class Lessons : Controller
    {
        private readonly CorsoContext _context;

        public Lessons(CorsoContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var corsoContext = _context.Lezionis.Include(l => l.IdLezioniNavigation);
            return View(await corsoContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lezionis == null)
            {
                return NotFound();
            }

            var lezioni = await _context.Lezionis
                .Include(l => l.IdLezioniNavigation)
                .FirstOrDefaultAsync(m => m.IdLezioni == id);
            if (lezioni == null)
            {
                return NotFound();
            }

            return View(lezioni);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["IdLezioni"] = new SelectList(_context.Insegnantis, "Lezione", "Lezione");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLezioni,CodCorso,Corso,Ore")] Lezioni lezioni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lezioni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLezioni"] = new SelectList(_context.Insegnantis, "Lezione", "Lezione", lezioni.IdLezioni);
            return View(lezioni);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lezionis == null)
            {
                return NotFound();
            }

            var lezioni = await _context.Lezionis.FindAsync(id);
            if (lezioni == null)
            {
                return NotFound();
            }
            ViewData["IdLezioni"] = new SelectList(_context.Insegnantis, "Lezione", "Lezione", lezioni.IdLezioni);
            return View(lezioni);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLezioni,CodCorso,Corso,Ore")] Lezioni lezioni)
        {
            if (id != lezioni.IdLezioni)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lezioni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LezioniExists(lezioni.IdLezioni))
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
            ViewData["IdLezioni"] = new SelectList(_context.Insegnantis, "Lezione", "Lezione", lezioni.IdLezioni);
            return View(lezioni);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lezionis == null)
            {
                return NotFound();
            }

            var lezioni = await _context.Lezionis
                .Include(l => l.IdLezioniNavigation)
                .FirstOrDefaultAsync(m => m.IdLezioni == id);
            if (lezioni == null)
            {
                return NotFound();
            }

            return View(lezioni);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lezionis == null)
            {
                return Problem("Entity set 'CorsoContext.Lezionis'  is null.");
            }
            var lezioni = await _context.Lezionis.FindAsync(id);
            if (lezioni != null)
            {
                _context.Lezionis.Remove(lezioni);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LezioniExists(int id)
        {
          return (_context.Lezionis?.Any(e => e.IdLezioni == id)).GetValueOrDefault();
        }
    }
}
