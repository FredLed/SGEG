using System;

using WebApp.BL.Interface;

namespace WebApp.BL
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public double MSRP { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string CUP { get; set; }
        
        public ICategory Category { get; set; }
    }
}