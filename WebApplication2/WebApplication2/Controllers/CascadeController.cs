using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebApplication2.DBContext;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CascadeController : Controller
    {
        private readonly FinalDBContext _context;
        Cascade cd = new Cascade();

        public CascadeController(FinalDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            cd.DerslerList = new SelectList(_context.Ders, "Id", "DersAdi");
            cd.OgretmenList = new SelectList(Enumerable.Empty<SelectListItem>()); // initially empty
            return View(cd);
        }

        public JsonResult GetTerritories(int Id)
        {
            var OgretmenlerList = (from CourseName in _context.Ogretmenler
                                   where CourseName.DersId == Id
                                   select new
                                   {
                                       Text = CourseName.Adi,
                                       Value = CourseName.Id
                                   }).ToList();

            return Json(OgretmenlerList);
        }
    }
}