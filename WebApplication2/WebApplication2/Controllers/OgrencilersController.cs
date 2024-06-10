using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApplication2.DBContext;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class OgrencilersController : Controller
    {
        private readonly FinalDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public OgrencilersController(FinalDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Ogrencilers
        public async Task<IActionResult> Index()
        {
            var model = new Ogrenciler();
            var finalDBContext = _context.Ogrenciler.Include(o => o.Sınıf);
            return View(await finalDBContext.ToListAsync());
        }

        // GET: Ogrencilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ogrenciler == null)
            {
                return NotFound();
            }

            var ogrenciler = await _context.Ogrenciler
                .Include(o => o.Sınıf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogrenciler == null)
            {
                return NotFound();
            }

            return View(ogrenciler);
        }

        // GET: Ogrencilers/Create
        public IActionResult Create()
        {
            ViewData["SınıfId"] = new SelectList(_context.Set<SINIFLAR>(), "Id", "sınıf");
            return View();
        }

        // POST: Ogrencilers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,Soyadi,VeliTelNo,Fotograf,ImageFile,SınıfId")] Ogrenciler ogrenciler)
        {
            if (ModelState.IsValid)
            {
                if (ogrenciler.ImageFile != null)
                {
                    string wwwrootpath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(ogrenciler.ImageFile.FileName);
                    string extension = Path.GetExtension(ogrenciler.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ogrenciler.Fotograf = "~/Contents/" + fileName;
                    string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await ogrenciler.ImageFile.CopyToAsync(filestream);
                    }
                }
                _context.Add(ogrenciler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SınıfId"] = new SelectList(_context.Set<SINIFLAR>(), "Id", "sınıf", ogrenciler.SınıfId);
            return View(ogrenciler);
        }

        // GET: Ogrencilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ogrenciler == null)
            {
                return NotFound();
            }

            var ogrenciler = await _context.Ogrenciler.FindAsync(id);
            if (ogrenciler == null)
            {
                return NotFound();
            }
            ViewData["SınıfId"] = new SelectList(_context.Set<SINIFLAR>(), "Id", "sınıf", ogrenciler.SınıfId);
            return View(ogrenciler);
        }

        // POST: Ogrencilers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Soyadi,VeliTelNo,Fotograf,ImageFile,SınıfId")] Ogrenciler ogrenciler)
        {
            ModelState.Remove("Sınıf");
            if (id != ogrenciler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudent = await _context.Ogrenciler.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

                    if (ogrenciler.ImageFile != null)
                    {
                        if (existingStudent != null && !string.IsNullOrEmpty(existingStudent.Fotograf))
                        {
                            string oldPath = Path.Combine(_hostEnvironment.WebRootPath, existingStudent.Fotograf.TrimStart('~').Replace('/', Path.DirectorySeparatorChar));
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }

                        string wwwrootpath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(ogrenciler.ImageFile.FileName);
                        string extension = Path.GetExtension(ogrenciler.ImageFile.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string newPath = Path.Combine(wwwrootpath + "/Contents/", fileName);
                        using (var filestream = new FileStream(newPath, FileMode.Create))
                        {
                            await ogrenciler.ImageFile.CopyToAsync(filestream);
                        }
                        ogrenciler.Fotograf = "~/Contents/" + fileName;

                        existingStudent.Fotograf = ogrenciler.Fotograf;
                    }
                    else
                    {
                        ogrenciler.Fotograf = existingStudent.Fotograf;
                    }

                    _context.Update(ogrenciler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgrencilerExists(ogrenciler.Id))
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
            ViewData["SınıfId"] = new SelectList(_context.Set<SINIFLAR>(), "Id", "sınıf", ogrenciler.SınıfId);
            return View(ogrenciler);
        }

        // GET: Ogrencilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ogrenciler == null)
            {
                return NotFound();
            }

            var ogrenciler = await _context.Ogrenciler
                .Include(o => o.Sınıf)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ogrenciler == null)
            {
                return NotFound();
            }

            return View(ogrenciler);
        }

        // POST: Ogrencilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ogrenciler == null)
            {
                return Problem("Entity set 'FinalDBContext.Ogrenciler'  is null.");
            }
            var ogrenciler = await _context.Ogrenciler.FindAsync(id);
            if (ogrenciler != null)
            {
                _context.Ogrenciler.Remove(ogrenciler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgrencilerExists(int id)
        {
            return (_context.Ogrenciler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
