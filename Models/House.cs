namespace SocialRentAccunting.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public float CommonArea { get; set; }
        public int FlatsCount { get; set; }

        public List<Contract> Contracts { get; set; } = new List<Contract>();

    }
}
