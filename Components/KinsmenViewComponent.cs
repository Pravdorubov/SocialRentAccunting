using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.Context;

namespace SocialRentAccunting.Components
{
    public class KinsmenViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public KinsmenViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var kinsmen = await _context.Kinsmen.Where(k => k.TenantId == id).ToListAsync();
            return View(kinsmen);
        }
    }
}
