using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DBContext;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class DerslersController : Controller
    {
        private readonly FinalDBContext _context;

        public DerslersController(FinalDBContext context)
        {
            _context = context;
        }

        // GET: Derslers
        public async Task<IActionResult> Index()
        {
            var finalDBContext = _context.Dersler.Include(d => d.Ders).Include(d => d.Ogretmen);
            return View(await finalDBContext.ToListAsync());
        }

        // GET: Derslers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dersler == null)
            {
                return NotFound();
            }

            var dersler = await _context.Dersler
                .Include(d => d.Ders)
                .Include(d => d.Ogretmen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dersler == null)
            {
                return NotFound();
            }

            return View(dersler);
        }

        // GET: Derslers/Create
        public IActionResult Create()
        {
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi");
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi");
            return View();
        }

        // POST: Derslers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DersId,Saat,OgretmenId")] Dersler dersler)
        {
            ModelState.Remove("Ogretmen");
            ModelState.Remove("Ders");
            if (ModelState.IsValid)
            {
                _context.Add(dersler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", dersler.DersId);
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi", dersler.OgretmenId);
            return View(dersler);
        }

        // GET: Derslers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dersler == null)
            {
                return NotFound();
            }

            var dersler = await _context.Dersler.FindAsync(id);
            if (dersler == null)
            {
                return NotFound();
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", dersler.DersId);
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi", dersler.OgretmenId);
            return View(dersler);
        }

        // POST: Derslers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DersId,Saat,OgretmenId")] Dersler dersler)
        {
            ModelState.Remove("Ogretmen");
            ModelState.Remove("Ders");
            if (id != dersler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dersler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DerslerExists(dersler.Id))
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
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", dersler.DersId);
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi", dersler.OgretmenId);
            return View(dersler);
        }

        // GET: Derslers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dersler == null)
            {
                return NotFound();
            }

            var dersler = await _context.Dersler
                .Include(d => d.Ders)
                .Include(d => d.Ogretmen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dersler == null)
            {
                return NotFound();
            }

            return View(dersler);
        }

        // POST: Derslers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dersler == null)
            {
                return Problem("Entity set 'FinalDBContext.Dersler'  is null.");
            }
            var dersler = await _context.Dersler.FindAsync(id);
            if (dersler != null)
            {
                _context.Dersler.Remove(dersler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DerslerExists(int id)
        {
          return (_context.Dersler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
