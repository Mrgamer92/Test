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
    public class Teachers : Controller
    {
        private readonly CorsoContext _context;

        public Teachers(CorsoContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
              return _context.Insegnantis != null ? 
                          View(await _context.Insegnantis.ToListAsync()) :
                          Problem("Entity set 'CorsoContext.Insegnantis'  is null.");
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insegnantis == null)
            {
                return NotFound();
            }

            var insegnanti = await _context.Insegnantis
                .FirstOrDefaultAsync(m => m.IdInsegnante == id);
            if (insegnanti == null)
            {
                return NotFound();
            }

            return View(insegnanti);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsegnante,Nome,Cognome,Lezione")] Insegnanti insegnanti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insegnanti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insegnanti);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insegnantis == null)
            {
                return NotFound();
            }

            var insegnanti = await _context.Insegnantis.FindAsync(id);
            if (insegnanti == null)
            {
                return NotFound();
            }
            return View(insegnanti);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInsegnante,Nome,Cognome,Lezione")] Insegnanti insegnanti)
        {
            if (id != insegnanti.IdInsegnante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insegnanti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsegnantiExists(insegnanti.IdInsegnante))
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
            return View(insegnanti);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insegnantis == null)
            {
                return NotFound();
            }

            var insegnanti = await _context.Insegnantis
                .FirstOrDefaultAsync(m => m.IdInsegnante == id);
            if (insegnanti == null)
            {
                return NotFound();
            }

            return View(insegnanti);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insegnantis == null)
            {
                return Problem("Entity set 'CorsoContext.Insegnantis'  is null.");
            }
            var insegnanti = await _context.Insegnantis.FindAsync(id);
            if (insegnanti != null)
            {
                _context.Insegnantis.Remove(insegnanti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsegnantiExists(int id)
        {
          return (_context.Insegnantis?.Any(e => e.IdInsegnante == id)).GetValueOrDefault();
        }
    }
}
