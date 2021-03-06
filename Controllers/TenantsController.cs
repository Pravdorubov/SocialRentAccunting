#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.Context;
using SocialRentAccunting.Models;
using SocialRentAccunting.ViewModels;

namespace SocialRentAccunting.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly AppDbContext _context;

        public TenantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tenants
        public IActionResult Index()
        {
            return View(_context.Tenants.Include(t=>t.Kinsmen).ToList());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.Include(t=>t.Kinsmen).FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            ViewBag.Kinships = _context.Kinships.ToList();

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TenantViewModel tenantModel)
        {
            if (ModelState.IsValid)
            {
                Tenant tenant = tenantModel.Tenant;
                List<Kinsman> kinsmen = tenantModel.Kinsmen.ToList();
                
                _context.Add(tenant);
                _context.SaveChanges();
                foreach (Kinsman kinsman in kinsmen)
                {
                    kinsman.TenantId = tenant.Id;
                    _context.Add(kinsman);
                }

                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(tenantModel);
        }

       

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants.Include(t=>t.Kinsmen).FirstOrDefaultAsync(t=>t.Id == id);
            var kinsmen = await _context.Kinsmen.Where(k=>k.Id == tenant.Id).ToListAsync();

            TenantViewModel tenantModel = new TenantViewModel();
            tenantModel.Tenant = tenant;
            tenantModel.Kinsmen = tenant.Kinsmen;


            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenantModel);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TenantViewModel tenantModel)
        {
            var tenant = tenantModel.Tenant;
            var kinsmen = tenantModel.Kinsmen;
            if (id != tenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                   await _context.SaveChangesAsync();

                    foreach (var kinsman in _context.Kinsmen.Where(k => k.TenantId == tenant.Id))
                    {
                        _context.Kinsmen.Remove(kinsman);
                        _context.Entry(kinsman).State = EntityState.Deleted;
                    }
                    await _context.SaveChangesAsync();

                    foreach (var kinsman in tenantModel.Kinsmen)
                    {
                        kinsman.TenantId = tenant.Id;
                        _context.Add(kinsman);
                    }
                    await _context.SaveChangesAsync();
                     
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.Id))
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
            return View(tenantModel);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }

        public IActionResult GetKinsmanCreateComponent(int count)
        {
            List<Kinship> kinships = _context.Kinships.ToList();
            return ViewComponent("KinsmanCreate", new {Count = count, Kinships = kinships});
        }

        public async Task<IActionResult> GetKinsmenComponent(int id)
        {
            return ViewComponent("Kinsmen", new {id = id});
        }

    }
}
