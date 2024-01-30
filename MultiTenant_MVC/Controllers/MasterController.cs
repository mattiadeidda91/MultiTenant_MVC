using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenant_MVC.DBContext;
using MultiTenant_MVC.Models.Entities;
using System.Data;

namespace MultiTenant_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MasterController : Controller
    {
        private readonly MasterDbContext _masterDbContext;

        public MasterController(MasterDbContext masterDbContext)
        {
            _masterDbContext = masterDbContext;
        }

        // GET: MasterController
        public async Task<IActionResult> Index()
        {
            var users = await _masterDbContext.Users.ToListAsync();
            return View(users);
        }

        // GET: MasterController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterUser = await _masterDbContext.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (masterUser == null)
            {
                return NotFound();
            }

            return View(masterUser);
        }

        // GET: MasterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MasterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,ConnectionString,IsActive")] MasterUser masterUser)
        {
            if (ModelState.IsValid)
            {
                _masterDbContext.Users.Add(masterUser);
                await _masterDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(masterUser);
        }

        // GET: MasterController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterUser = await _masterDbContext.Users.FindAsync(id);
            if (masterUser == null)
            {
                return NotFound();
            }

            return View(masterUser);
        }

        // POST: MasterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,ConnectionString,IsActive")] MasterUser masterUser)
        {
            if (id != masterUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _masterDbContext.Users.Update(masterUser);
                    await _masterDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterUserExists(masterUser.Id))
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
            return View(masterUser);
        }

        // GET: MasterController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterUser = await _masterDbContext.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (masterUser == null)
            {
                return NotFound();
            }

            return View(masterUser);
        }

        // POST: MasterController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masterUser = await _masterDbContext.Users.FindAsync(id);

            if (masterUser != null)
            {
                _masterDbContext.Users.Remove(masterUser);
                await _masterDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(masterUser);
        }

        private bool MasterUserExists(int id)
        {
            return _masterDbContext.Users.Any(e => e.Id == id);
        }
    }
}
