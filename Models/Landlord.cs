using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class Landlord
    {
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Руководитель")]
        public string Head { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
