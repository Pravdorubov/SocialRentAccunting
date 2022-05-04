using SocialRentAccunting.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialRentAccunting.ViewModels
{
    [NotMapped]
    public class TenantViewModel
    {
        public Tenant Tenant { get; set; }
        public ICollection<Kinsman> Kinsmen { get; set; }
    }
}
