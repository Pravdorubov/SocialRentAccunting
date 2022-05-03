using SocialRent.Models;

namespace SocialRentAccunting.ViewModels
{
    public class TenantViewModel
    {
        public Tenant Tenant { get; set; }
        public ICollection<Kinsman> Kinsmen { get; set; }
    }
}
