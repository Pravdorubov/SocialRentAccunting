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
            List<TenantSearchModel> model = new List<TenantSearchModel>();
            foreach (var tenant in _context.Tenants)
            {
                TenantSearchModel item = new TenantSearchModel();
                item.Id = tenant.Id;
                item.FullName = tenant.FullName;
                item.BirthDate = tenant.BirthDate;
                item.Phone = tenant.Phone;
                item.Passport = $"{tenant.Passport.Serie} {tenant.Passport.Number}";

                model.Add(item);

            }
            return View(model);
        }
    }
}
