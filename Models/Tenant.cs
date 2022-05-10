using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        
        [Display(Name = "Паспорт")]
        public Passport? Passport { get; set; }

        [Display(Name = "Родственники")]
        public virtual ICollection<Kinsman> Kinsmen { get; set; } = new List<Kinsman>();
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    }
}
