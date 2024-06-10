using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DBContext;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class OgretmenlersController : Controller
    {
        private readonly FinalDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OgretmenlersController(FinalDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Ogretmenlers
        public async Task<IActionResult> Index()
        {
            var finalDBContext = _context.Ogretmenler.Include(o => o.DersAd);
            return View(await finalDBContext.ToListAsync());
        }

        // GET: Ogretmenlers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ogretmenler == null)
            {
                return NotFound();
            }

            var ogretmenler = await _context.Ogretmenler
                .Include(o => o.DersAd)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogretmenler == null)
            {
                return NotFound();
            }

            return View(ogretmenler);
        }

        // GET: Ogretmenlers/Create
        public IActionResult Create()
        {
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi");
            return View();
        }

        // POST: Ogretmenlers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,Soyadi,TelNo,Fotograf,ImageFile,DersId")] Ogretmenler ogretmenler)
        {
            ModelState.Remove("DersAd");
            if (ModelState.IsValid)
            {
                if (ogretmenler.ImageFile != null)
                {
                    string wwwrootpath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(ogretmenler.ImageFile.FileName);
                    string extension = Path.GetExtension(ogretmenler.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ogretmenler.Fotograf = "~/Contents/" + fileName;
                    string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await ogretmenler.ImageFile.CopyToAsync(filestream);
                    }
                }

                _context.Add(ogretmenler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", ogretmenler.DersId);
            return View(ogretmenler);
        }

        // GET: Ogretmenlers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ogretmenler == null)
            {
                return NotFound();
            }

            var ogretmenler = await _context.Ogretmenler.FindAsync(id);
            if (ogretmenler == null)
            {
                return NotFound();
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", ogretmenler.DersId);
            return View(ogretmenler);
        }

        // POST: Ogretmenlers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Soyadi,TelNo,Fotograf,ImageFile,DersId")] Ogretmenler ogretmenler)
        {
            ModelState.Remove("DersAd");
            if (id != ogretmenler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingTeacher = await _context.Ogretmenler.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                    if (existingTeacher != null && !string.IsNullOrEmpty(existingTeacher.Fotograf))
                    {
                        string oldPath = Path.Combine(_hostEnvironment.WebRootPath, existingTeacher.Fotograf.TrimStart('~').Replace('/', Path.DirectorySeparatorChar));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    if (ogretmenler.ImageFile != null)
                    {
                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(ogretmenler.ImageFile.FileName);
                        string extension = Path.GetExtension(ogretmenler.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yyMMddhhmmssfff") + extension;
                        ogretmenler.Fotograf = "~/Contents/" + fileName;
                        string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await ogretmenler.ImageFile.CopyToAsync(filestream);
                        }
                    }

                    _context.Update(ogretmenler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgretmenlerExists(ogretmenler.Id))
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
            ViewData["DersId"] = new SelectList(_context.Ders, "Id", "DersAdi", ogretmenler.DersId);
            return View(ogretmenler);
        }

        // GET: Ogretmenlers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ogretmenler == null)
            {
                return NotFound();
            }

            var ogretmenler = await _context.Ogretmenler
                .Include(o => o.DersAd)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogretmenler == null)
            {
                return NotFound();
            }

            return View(ogretmenler);
        }

        // POST: Ogretmenlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ogretmenler == null)
            {
                return Problem("Entity set 'FinalDBContext.Ogretmenler'  is null.");
            }
            var ogretmenler = await _context.Ogretmenler.FindAsync(id);
            if (ogretmenler != null)
            {
                _context.Ogretmenler.Remove(ogretmenler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgretmenlerExists(int id)
        {
            return (_context.Ogretmenler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
