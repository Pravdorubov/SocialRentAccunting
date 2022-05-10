using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class Passport
    {
        [Display(Name = "Серия паспорта")]
        public string Serie { get; set; }
        [Display(Name = "Номер паспорта")]
        public string Number { get; set; }
    }
}