using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.Context;
using SocialRentAccunting.ViewModels;

namespace SocialRentAccunting.Components
{
    public class LandlordFindViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public LandlordFindViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_context.Landlords);
        }
    }
}
