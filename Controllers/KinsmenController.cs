#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRent.Models;
using SocialRentAccunting.Context;

namespace SocialRentAccunting.Controllers
{
    public class KinsmenController : Controller
    {
        private readonly AppDbContext _context;

        public KinsmenController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Kinsmen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kinsmen.ToListAsync());
        }

        // GET: Kinsmen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinsman = await _context.Kinsmen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kinsman == null)
            {
                return NotFound();
            }

            return View(kinsman);
        }

        // GET: Kinsmen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kinsmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,TenantId,KinshipId")] Kinsman kinsman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kinsman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kinsman);
        }

        // GET: Kinsmen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinsman = await _context.Kinsmen.FindAsync(id);
            if (kinsman == null)
            {
                return NotFound();
            }
            return View(kinsman);
        }

        // POST: Kinsmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,TenantId,KinshipId")] Kinsman kinsman)
        {
            if (id != kinsman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kinsman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KinsmanExists(kinsman.Id))
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
            return View(kinsman);
        }

        // GET: Kinsmen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinsman = await _context.Kinsmen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kinsman == null)
            {
                return NotFound();
            }

            return View(kinsman);
        }

        // POST: Kinsmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kinsman = await _context.Kinsmen.FindAsync(id);
            _context.Kinsmen.Remove(kinsman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KinsmanExists(int id)
        {
            return _context.Kinsmen.Any(e => e.Id == id);
        }
    }
}
