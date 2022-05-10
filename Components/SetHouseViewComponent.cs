using Microsoft.AspNetCore.Mvc;
using SocialRentAccunting.Models;

namespace SocialRentAccunting.Components
{
    public class SetHouseViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(House house)
        {
            return View(house);
        }

    }
}
