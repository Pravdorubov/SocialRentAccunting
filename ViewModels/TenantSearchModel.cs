using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.ViewModels
{
    public class TenantSearchModel
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }
    }
}
