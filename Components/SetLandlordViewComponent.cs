using Microsoft.AspNetCore.Mvc;
using SocialRentAccunting.Models;

namespace SocialRentAccunting.Components
{
    public class SetLandlordViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Landlord landlord)
        {
            return View(landlord);
        }

    }
}
