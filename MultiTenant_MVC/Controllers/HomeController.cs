using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenant_MVC.DBContext;
using MultiTenant_MVC.Models.Entities;
using System.Security.Claims;

namespace MultiTenant_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly MasterDbContext _masterContext;

        public HomeController(MasterDbContext masterContext)
        {
            _masterContext = masterContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = await _masterContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (IsValidUser(user, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user!.Name!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim("ConnectionString", user.ConnectionString!),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Imposta la persistenza dell'autenticazione
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                //var tenant = await _masterContext.Tenants.FirstOrDefaultAsync(t => t.UserId == user!.Id);
                //if(tenant != null) 
                //{
                //    var connectionString = tenant.DatabaseConnectionString ?? string.Empty;
                //    _tenantContext = _tenantFactory.GetTenantContext(connectionString);
                //}

                if(user.IsAdmin) 
                    return Redirect("/Master");
                else
                    return Redirect("/Order");
            }
            else
            {
                // L'autenticazione ha fallito, mostra un messaggio di errore
                ModelState.AddModelError(string.Empty, "Credenziali non valide.");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }

        private bool IsValidUser(MasterUser? user, string psw)
        {         
            if (user != null && user.Password == psw)
                return true;
            else
                return false;
        }
    }
}