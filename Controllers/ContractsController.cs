using jQuery_Ajax_CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.Context;
using SocialRentAccunting.DocCreator;
using SocialRentAccunting.Models;
using SocialRentAccunting.ViewModels;

namespace SocialRentAccunting.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;

        public ContractsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateDoc(int id)
        {
            var contract = _context.Contracts
                .Include(c => c.Tenant).ThenInclude(t => t.Kinsmen)
                .Include(c => c.Order)
                .Include(c => c.Landlord)
                .Include(c => c.House)
                .FirstOrDefault(c => c.Id == id);

            DocumentCreate.ReplaceTextWithText(contract, _context.Kinships.ToList());

            return View("Index", _context.Contracts.Include(c => c.House).Include(c => c.Landlord).Include(c => c.Tenant));
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Contracts.Include(c => c.House).Include(c => c.Landlord).Include(c => c.Tenant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.House)
                .Include(c => c.Landlord)
                .Include(c => c.Tenant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }


            return View(contract);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "Id");
            ViewData["LandlordId"] = new SelectList(_context.Landlords, "Id", "Id");
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "Id");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "Id", contract.HouseId);
            ViewData["LandlordId"] = new SelectList(_context.Landlords, "Id", "Id", contract.LandlordId);
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "Id", contract.TenantId);
            return View(contract);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.Include(c=>c.Order).Include(c=>c.Landlord).Include(c=>c.House).Include(c=>c.Tenant).FirstOrDefaultAsync(c=>c.Id==id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "Id", contract.HouseId);
            ViewData["LandlordId"] = new SelectList(_context.Landlords, "Id", "Id", contract.LandlordId);
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "Id", contract.TenantId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DateStart,DateEnd,TenantId,HouseId,LandlordId")] Contract contract)
        {
            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
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
            ViewData["HouseId"] = new SelectList(_context.Houses, "Id", "Id", contract.HouseId);
            ViewData["LandlordId"] = new SelectList(_context.Landlords, "Id", "Id", contract.LandlordId);
            ViewData["TenantId"] = new SelectList(_context.Tenants, "Id", "Id", contract.TenantId);
            return View(contract);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.House)
                .Include(c => c.Landlord)
                .Include(c => c.Tenant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }

        public IActionResult GetTenantSearchComponent()
        {
            return ViewComponent("TenantFind");
        }

        public async Task<IActionResult> SetTenantComponent(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            return ViewComponent("SetTenant", new { tenant = tenant });
        }

        public IActionResult GetLandlordSearchComponent()
        {
            return ViewComponent("LandlordFind");
        }

        public async Task<IActionResult> SetLandlordComponent(int id)
        {
            var landlord = await _context.Landlords.FindAsync(id);
            return ViewComponent("SetLandlord", new { landlord = landlord });
        }

        public IActionResult GetHouseSearchComponent()
        {
            return ViewComponent("HouseFind");
        }

        public async Task<IActionResult> SetHouseComponent(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            return ViewComponent("SetHouse", new { house = house });
        }

        public IActionResult CreateTenant()
        {
            return PartialView("_CreateTenant", new TenantViewModel());
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> CreateTenant(TenantViewModel tenantModel)
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

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Create"), tenantId = tenant.Id });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_CreateTenant", tenantModel) });
        }

        // GET: Houses/Create
        public IActionResult CreateHouse()
        {
            return PartialView("_CreateHouse", new House());
        }

        // POST: Houses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> CreateHouse(House house)
        {
            if (ModelState.IsValid)
            {
                _context.Add(house);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Create"), houseId = house.Id });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_CreateHouse", house) });
        }


        // GET: Landlords/Create
        public IActionResult CreateLandlord()
        {
            return PartialView("_CreateLandlord", new Landlord());
        }

        // POST: Landlords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> CreateLandlord(Landlord landlord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(landlord);
                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Create"), landlordId = landlord.Id });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_CreateLandlord", landlord) });
        }

    }
}
