using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class Kinship
    {
        public int Id { get; set; }
        [Display(Name = "Родство")]
        public string Name { get; set; }

    }
}