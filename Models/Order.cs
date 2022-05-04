namespace SocialRentAccunting.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; internal set; }
    }
}