using Microsoft.AspNetCore.Mvc;
using SocialRentAccunting.Context;
using SocialRentAccunting.Models;
using SocialRentAccunting.ViewModels;
using SocialRentAccunting.ViewModels.HelperModels;

namespace SocialRentAccunting.Components
{
    public class KinsmanEditViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public KinsmanEditViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count, Kinsman kinsman)
        {
            ViewBag.Count = count;
            ViewBag.Name = $"Kinsman[{count}].KinshipId";

            List<SelectListModel> selectModel = new List<SelectListModel>();
            foreach (Kinship kinship in _context.Kinships)
            {
                SelectListModel item = new SelectListModel(kinship.Id, kinship.Name);
                selectModel.Add(item);
            }

            selectModel.Insert(0, new SelectListModel(-1, "-----" ));

            KismanEditViewComponentModel model = new KismanEditViewComponentModel();
            model.SelectListModel = selectModel;
            model.Kinsman = kinsman;

            return View(model);
        }
    }
}
