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
    public class Students : Controller
    {
        private readonly CorsoContext _context;

        public Students(CorsoContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Allievos != null ? 
                          View(await _context.Allievos.ToListAsync()) :
                          Problem("Entity set 'CorsoContext.Allievos'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Allievos == null)
            {
                return NotFound();
            }

            var allievo = await _context.Allievos
                .FirstOrDefaultAsync(m => m.IdAllievo == id);
            if (allievo == null)
            {
                return NotFound();
            }

            return View(allievo);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAllievo,Nome,Cognome,CorsoScelto")] Allievo allievo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allievo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(allievo);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Allievos == null)
            {
                return NotFound();
            }

            var allievo = await _context.Allievos.FindAsync(id);
            if (allievo == null)
            {
                return NotFound();
            }
            return View(allievo);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAllievo,Nome,Cognome,CorsoScelto")] Allievo allievo)
        {
            if (id != allievo.IdAllievo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allievo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllievoExists(allievo.IdAllievo))
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
            return View(allievo);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Allievos == null)
            {
                return NotFound();
            }

            var allievo = await _context.Allievos
                .FirstOrDefaultAsync(m => m.IdAllievo == id);
            if (allievo == null)
            {
                return NotFound();
            }

            return View(allievo);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Allievos == null)
            {
                return Problem("Entity set 'CorsoContext.Allievos'  is null.");
            }
            var allievo = await _context.Allievos.FindAsync(id);
            if (allievo != null)
            {
                _context.Allievos.Remove(allievo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllievoExists(int id)
        {
          return (_context.Allievos?.Any(e => e.IdAllievo == id)).GetValueOrDefault();
        }
    }
}
