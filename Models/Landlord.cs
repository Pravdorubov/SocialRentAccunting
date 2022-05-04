namespace SocialRentAccunting.Models
{
    public class Landlord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Head { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
