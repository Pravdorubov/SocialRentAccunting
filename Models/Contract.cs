using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class Contract
    {
        public int Id { get; set; }
        [Display(Name = "Номер")]
        public string Number { get; set; }
        [Display(Name = "Дата заключения")]
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [Display(Name = "Дата расторжения")]
        [DataType(DataType.Date)]
        public DateTime? DateEnd { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int TenantId { get; set; }
        [Display(Name = "Наниматель")]
        public Tenant? Tenant { get; set; }

        public int HouseId { get; set; }
        [Display(Name = "Помещение")]
        public House? House { get; set; }

        public int LandlordId { get; set; }
        [Display(Name = "Наймодатель")]
        public Landlord? Landlord { get; set; }
    }
}
