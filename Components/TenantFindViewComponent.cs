using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.Context;
using SocialRentAccunting.ViewModels;

namespace SocialRentAccunting.Components
{
    public class TenantFindViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public TenantFindViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await _context.Tenants.ToListAsync());
        }
    }
}
