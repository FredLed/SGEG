using System;

using WebApp.BL.Interface;

namespace WebApp.BL
{
    public class Item : IItem
    {
        public Guid Id { get; set; }
        
        public IProduct Product { get; set; }

        public double Cost { get; set; }

        public string SerialNumber { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime ReceptionDate { get; set; }
    }
}