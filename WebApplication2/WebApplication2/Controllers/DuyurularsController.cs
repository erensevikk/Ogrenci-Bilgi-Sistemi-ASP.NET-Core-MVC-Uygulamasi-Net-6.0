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
    public class DuyurularsController : Controller
    {
        private readonly FinalDBContext _context;

        public DuyurularsController(FinalDBContext context)
        {
            _context = context;
        }

        // GET: Duyurulars
        public async Task<IActionResult> Index()
        {
            var model = new Duyurular
            {
                Tarih = DateTime.Now 
            };
            var finalDBContext = _context.Duyurular.Include(d => d.Ogretmen).Include(d => d.Yer);
            return View(await finalDBContext.ToListAsync());
        }

        // GET: Duyurulars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Duyurular == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Duyurular
                .Include(d => d.Ogretmen)
                .Include(d => d.Yer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duyurular == null)
            {
                return NotFound();
            }

            return View(duyurular);
        }

        // GET: Duyurulars/Create
        public IActionResult Create()
        {
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi");
            ViewData["YerId"] = new SelectList(_context.Yerlers, "Id", "mekan");
            return View();
        }

        // POST: Duyurulars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OgretmenId,Duyuru,Tarih,YerId")] Duyurular duyurular)
        {
            ModelState.Remove("Ogretmen");
            ModelState.Remove("Yer");
            if (ModelState.IsValid)
            {
                _context.Add(duyurular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi", duyurular.OgretmenId);
            ViewData["YerId"] = new SelectList(_context.Yerlers, "Id", "mekan", duyurular.YerId);
            return View(duyurular);
        }

        // GET: Duyurulars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Duyurular == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Duyurular.FindAsync(id);
            if (duyurular == null)
            {
                return NotFound();
            }
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi", duyurular.OgretmenId);
            ViewData["YerId"] = new SelectList(_context.Yerlers, "Id", "mekan", duyurular.YerId);
            return View(duyurular);
        }

        // POST: Duyurulars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OgretmenId,Duyuru,Tarih,YerId")] Duyurular duyurular)
        {
            ModelState.Remove("Ogretmen");
            ModelState.Remove("Yer");
            if (id != duyurular.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duyurular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuyurularExists(duyurular.Id))
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
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmenler, "Id", "Adi", duyurular.OgretmenId);
            ViewData["YerId"] = new SelectList(_context.Yerlers, "Id", "mekan", duyurular.YerId);
            return View(duyurular);
        }

        // GET: Duyurulars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Duyurular == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Duyurular
                .Include(d => d.Ogretmen)
                .Include(d => d.Yer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duyurular == null)
            {
                return NotFound();
            }

            return View(duyurular);
        }

        // POST: Duyurulars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Duyurular == null)
            {
                return Problem("Entity set 'FinalDBContext.Duyurular'  is null.");
            }
            var duyurular = await _context.Duyurular.FindAsync(id);
            if (duyurular != null)
            {
                _context.Duyurular.Remove(duyurular);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuyurularExists(int id)
        {
          return (_context.Duyurular?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
