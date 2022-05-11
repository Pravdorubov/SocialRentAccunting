using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialRentAccunting.Context;
using SocialRentAccunting.Models;
using SocialRentAccunting.ViewModels;

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
            List<Kinship> kinships = _context.Kinships.ToList();

            List<KinsmanKinshipModel> kinsmenModel = kinsmen.Select(k =>
                new KinsmanKinshipModel()
                {
                    FullName = k.FullName,
                    Kinship = kinships.FirstOrDefault(s => s.Id == k.KinshipId)?.Name ?? string.Empty
                }
            ).ToList();
            return View(kinsmenModel);
        }
    }
}
