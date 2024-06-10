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
    public class BelgelerController : Controller
    {
        private readonly FinalDBContext _context;

        public BelgelerController(FinalDBContext context)
        {
            _context = context;
        }

        // GET: Belgeler
        public async Task<IActionResult> Index()
        {
            var model = new Belgeler();
            var finalDBContext = _context.Belgeler.Include(b => b.Ogrenci);
            return View(await finalDBContext.ToListAsync());
        }

        // GET: Belgeler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Belgeler == null)
            {
                return NotFound();
            }

            var belgeler = await _context.Belgeler
                .Include(b => b.Ogrenci)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (belgeler == null)
            {
                return NotFound();
            }

            return View(belgeler);
        }

        // GET: Belgeler/Create
        public IActionResult Create()
        {
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi");
            return View();
        }

        // POST: Belgeler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OgrenciId,OgrenciSoyad,tskbelge,takdirbelge,onurbelge,basaribelge")] Belgeler belgeler)
        {
            ModelState.Remove("Ogrenci");
            if (ModelState.IsValid)
            {
                _context.Add(belgeler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi", belgeler.OgrenciId);
            return View(belgeler);
        }

        // GET: Belgeler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Belgeler == null)
            {
                return NotFound();
            }

            var belgeler = await _context.Belgeler.FindAsync(id);
            if (belgeler == null)
            {
                return NotFound();
            }
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi", belgeler.OgrenciId);
            return View(belgeler);
        }

        // POST: Belgeler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OgrenciId,OgrenciSoyad,tskbelge,takdirbelge,onurbelge,basaribelge")] Belgeler belgeler)
        {
            ModelState.Remove("Ogrenci");
            if (id != belgeler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(belgeler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BelgelerExists(belgeler.Id))
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
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi", belgeler.OgrenciId);
            return View(belgeler);
        }

        // GET: Belgeler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Belgeler == null)
            {
                return NotFound();
            }

            var belgeler = await _context.Belgeler
                .Include(b => b.Ogrenci)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (belgeler == null)
            {
                return NotFound();
            }

            return View(belgeler);
        }

        // POST: Belgeler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Belgeler == null)
            {
                return Problem("Entity set 'FinalDBContext.Belgeler'  is null.");
            }
            var belgeler = await _context.Belgeler.FindAsync(id);
            if (belgeler != null)
            {
                _context.Belgeler.Remove(belgeler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BelgelerExists(int id)
        {
          return (_context.Belgeler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
