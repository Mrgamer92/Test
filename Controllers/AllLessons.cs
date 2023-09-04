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
    public class AllLessons : Controller
    {
        private readonly CorsoContext _context;

        public AllLessons(CorsoContext context)
        {
            _context = context;
        }

        // GET: AllLessons
        public async Task<IActionResult> Index()
        {
            var corsoContext = _context.AllieviLeziones.Include(a => a.AllievoNavigation).Include(a => a.IdLezioneNavigation);
            return View(await corsoContext.ToListAsync());
        }

        // GET: AllLessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllieviLeziones == null)
            {
                return NotFound();
            }

            var allieviLezione = await _context.AllieviLeziones
                .Include(a => a.AllievoNavigation)
                .Include(a => a.IdLezioneNavigation)
                .FirstOrDefaultAsync(m => m.Allievo == id);
            if (allieviLezione == null)
            {
                return NotFound();
            }

            return View(allieviLezione);
        }

        // GET: AllLessons/Create
        public IActionResult Create()
        {
            ViewData["Allievo"] = new SelectList(_context.Allievos, "IdAllievo", "IdAllievo");
            ViewData["IdLezione"] = new SelectList(_context.Lezionis, "IdLezioni", "IdLezioni");
            return View();
        }

        // POST: AllLessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLezione,Allievo")] AllieviLezione allieviLezione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allieviLezione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Allievo"] = new SelectList(_context.Allievos, "IdAllievo", "IdAllievo", allieviLezione.Allievo);
            ViewData["IdLezione"] = new SelectList(_context.Lezionis, "IdLezioni", "IdLezioni", allieviLezione.IdLezione);
            return View(allieviLezione);
        }

        // GET: AllLessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllieviLeziones == null)
            {
                return NotFound();
            }

            var allieviLezione = await _context.AllieviLeziones.FindAsync(id);
            if (allieviLezione == null)
            {
                return NotFound();
            }
            ViewData["Allievo"] = new SelectList(_context.Allievos, "IdAllievo", "IdAllievo", allieviLezione.Allievo);
            ViewData["IdLezione"] = new SelectList(_context.Lezionis, "IdLezioni", "IdLezioni", allieviLezione.IdLezione);
            return View(allieviLezione);
        }

        // POST: AllLessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLezione,Allievo")] AllieviLezione allieviLezione)
        {
            if (id != allieviLezione.Allievo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allieviLezione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllieviLezioneExists(allieviLezione.Allievo))
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
            ViewData["Allievo"] = new SelectList(_context.Allievos, "IdAllievo", "IdAllievo", allieviLezione.Allievo);
            ViewData["IdLezione"] = new SelectList(_context.Lezionis, "IdLezioni", "IdLezioni", allieviLezione.IdLezione);
            return View(allieviLezione);
        }

        // GET: AllLessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllieviLeziones == null)
            {
                return NotFound();
            }

            var allieviLezione = await _context.AllieviLeziones
                .Include(a => a.AllievoNavigation)
                .Include(a => a.IdLezioneNavigation)
                .FirstOrDefaultAsync(m => m.Allievo == id);
            if (allieviLezione == null)
            {
                return NotFound();
            }

            return View(allieviLezione);
        }

        // POST: AllLessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AllieviLeziones == null)
            {
                return Problem("Entity set 'CorsoContext.AllieviLeziones'  is null.");
            }
            var allieviLezione = await _context.AllieviLeziones.FindAsync(id);
            if (allieviLezione != null)
            {
                _context.AllieviLeziones.Remove(allieviLezione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllieviLezioneExists(int id)
        {
          return (_context.AllieviLeziones?.Any(e => e.Allievo == id)).GetValueOrDefault();
        }
    }
}
