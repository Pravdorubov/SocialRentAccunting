using Microsoft.AspNetCore.Mvc;
using SocialRentAccunting.Models;
using SocialRentAccunting.ViewModels.HelperModels;

namespace SocialRentAccunting.Components
{
    public class KinsmanCreateViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count, List<Kinship> kinships)
        {
            ViewBag.Count = count - 1;

            List<SelectListModel> selectModel = new List<SelectListModel>();
            foreach (Kinship kinship in kinships)
            {
                SelectListModel item = new SelectListModel(kinship.Id, kinship.Name);
                selectModel.Add(item);
            }

            selectModel.Insert(0, new SelectListModel(-1, "Родство" ));

            return View(selectModel);
        }
    }
}
