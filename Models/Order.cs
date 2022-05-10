using System.ComponentModel.DataAnnotations;

namespace SocialRentAccunting.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "����� ������������")]
        public string Number { get; set; }
        [Display(Name = "���� ������������")]
        public DateTime Date { get; set; }

        public Contract? Contract { get; set; }
    }
}