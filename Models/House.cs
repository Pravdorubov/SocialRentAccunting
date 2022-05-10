using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class House
    {
        public int Id { get; set; }
        [Display(Name = "Кадастровый номер")]
        public string CadastreNumber { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Общая площадь")]
        public float CommonArea { get; set; }
        [Display(Name = "Количество комнат")]
        public int FlatsCount { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();

    }
}
