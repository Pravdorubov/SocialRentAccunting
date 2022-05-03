﻿using System;

namespace SocialRent.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Order Order { get; set; }
       
        public int TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        
        public int HouseId { get; set; }
        public House? House { get; set; }
        
        public int LandlordId {  get; set; }
        public Landlord? Landlord { get; set; }
    }
}