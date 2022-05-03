namespace SocialRent.Models
{
    public class Kinsman
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        
        public int KinshipId { get; set; }
        //public Kinship? Kinship { get; set; }
    }
}
