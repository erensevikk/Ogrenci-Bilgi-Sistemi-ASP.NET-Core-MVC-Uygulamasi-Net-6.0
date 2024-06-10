using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.DBContext;

namespace WebApplication2.Controllers
{
    public class StartpController : Controller
    {
        private readonly FinalDBContext _context;

        public StartpController(FinalDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Logincs logincs)
        {
            var user = _context.UserProfiles
                .FirstOrDefault(u => u.UserName == logincs.Email && u.Sifre == logincs.PassWord);

            if (user != null)
            {
                user.IsActive = logincs.LoggedStatus;
                _context.Update(user);
                await _context.SaveChangesAsync();

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, logincs.Email),
                    new Claim("DiğerÖzellikler", "Örnek Rol")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = user.IsActive
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), prop);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["OnayMesaji"] = "Kullanıcı Bulunamadı";
            }

            return View();
        }
    }
}
