using Microsoft.AspNetCore.Mvc;
using SocialRentAccunting.Models;

namespace SocialRentAccunting.Components
{
    public class SetTenantViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Tenant tenant)
        {
            return View(tenant);
        }

    }
}
