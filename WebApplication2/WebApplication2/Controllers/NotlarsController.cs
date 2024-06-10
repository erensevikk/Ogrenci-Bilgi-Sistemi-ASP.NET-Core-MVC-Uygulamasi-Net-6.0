using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DBContext;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class NotlarsController : Controller
    {
        private readonly FinalDBContext _context;

        public NotlarsController(FinalDBContext context)
        {
            _context = context;
        }

        // GET: Notlars
        public async Task<IActionResult> Index()
        {
            var model = new Notlar();
            var finalDBContext = _context.Not.Include(n => n.DersAd).Include(n => n.Ogrenci);
            return View(await finalDBContext.ToListAsync());
        }

        // GET: Notlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Not == null)
            {
                return NotFound();
            }

            var notlar = await _context.Not
                .Include(n => n.DersAd)
                .Include(n => n.Ogrenci)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notlar == null)
            {
                return NotFound();
            }

            return View(notlar);
        }

        // GET: Notlars/Create
        public IActionResult Create()
        {
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi");
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi");
            return View();
        }

        // POST: Notlars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OgrenciId,DersId,NotDegeri,sonuc")] Notlar notlar)
        {
            ModelState.Remove("DersAd");
            ModelState.Remove("Ogrenci");

            notlar.sonuc = notlar.NotDegeri >= 60;

            if (ModelState.IsValid)
            {
                _context.Add(notlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", notlar.DersId);
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi", notlar.OgrenciId);
            return View(notlar);
        }

        // GET: Notlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Not == null)
            {
                return NotFound();
            }

            var notlar = await _context.Not.FindAsync(id);
            if (notlar == null)
            {
                return NotFound();
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", notlar.DersId);
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi", notlar.OgrenciId);
            return View(notlar);
        }

        // POST: Notlars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OgrenciId,DersId,NotDegeri,sonuc")] Notlar notlar)
        {
            ModelState.Remove("DersAd");
            ModelState.Remove("Ogrenci");

            if (id != notlar.Id)
            {
                return NotFound();
            }

            // Set the sonuc value based on NotDegeri
            notlar.sonuc = notlar.NotDegeri >= 60;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotlarExists(notlar.Id))
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
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", notlar.DersId);
            ViewData["OgrenciId"] = new SelectList(_context.Ogrenciler, "Id", "Adi", notlar.OgrenciId);
            return View(notlar);
        }

        // GET: Notlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Not == null)
            {
                return NotFound();
            }

            var notlar = await _context.Not
                .Include(n => n.DersAd)
                .Include(n => n.Ogrenci)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notlar == null)
            {
                return NotFound();
            }

            return View(notlar);
        }

        // POST: Notlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Not == null)
            {
                return Problem("Entity set 'FinalDBContext.Not'  is null.");
            }
            var notlar = await _context.Not.FindAsync(id);
            if (notlar != null)
            {
                _context.Not.Remove(notlar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotlarExists(int id)
        {
            return (_context.Not?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
