using System;
using System.Collections.Generic;

namespace SocialRent.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Order { get; set; }
        public DateTime OrderDate { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }

        public Passport Passport { get; set; }

        public List<Kinsman> Kinsmen { get; set; } = new List<Kinsman>();
        public List<Contract> Contracts { get; set; } = new List<Contract>();

    }
}
