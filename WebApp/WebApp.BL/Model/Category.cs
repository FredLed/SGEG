using System;
using System.Collections.Generic;

using WebApp.BL.Interface;

namespace WebApp.BL
{
    public class Category : ICategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public ICategory ParentCategory { get; set; }
        
        public List<ICategory> SubCategories { get; set; }
    }
}