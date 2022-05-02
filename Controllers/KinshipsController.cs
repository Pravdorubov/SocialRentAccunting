#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialRent.Models;
using SocialRentAccunting.Context;

namespace SocialRentAccunting.Controllers
{
    public class KinshipsController : Controller
    {
        private readonly AppDbContext _context;

        public KinshipsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Kinships
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kinships.ToListAsync());
        }

        // GET: Kinships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinship = await _context.Kinships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kinship == null)
            {
                return NotFound();
            }

            return View(kinship);
        }

        // GET: Kinships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kinships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Kinship kinship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kinship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kinship);
        }

        // GET: Kinships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinship = await _context.Kinships.FindAsync(id);
            if (kinship == null)
            {
                return NotFound();
            }
            return View(kinship);
        }

        // POST: Kinships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Kinship kinship)
        {
            if (id != kinship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kinship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KinshipExists(kinship.Id))
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
            return View(kinship);
        }

        // GET: Kinships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kinship = await _context.Kinships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kinship == null)
            {
                return NotFound();
            }

            return View(kinship);
        }

        // POST: Kinships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kinship = await _context.Kinships.FindAsync(id);
            _context.Kinships.Remove(kinship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KinshipExists(int id)
        {
            return _context.Kinships.Any(e => e.Id == id);
        }
    }
}
