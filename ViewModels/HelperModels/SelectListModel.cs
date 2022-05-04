namespace SocialRentAccunting.ViewModels.HelperModels
{
    public class SelectListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SelectListModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
