using Microsoft.AspNetCore.Mvc;

namespace SocialRentAccunting.Components
{
    public class CreateTenantViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
