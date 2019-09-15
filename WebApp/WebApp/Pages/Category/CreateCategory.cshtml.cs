using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.BL.Interface;

namespace WebApp.Pages.Category
{
    public class CreateCategoryModel : PageModel
    {

        public CreateCategoryModel()
        {
        }

        public CreateCategoryModel(ICategory category, IEnumerable<ICategory> categories)
        {
            _id = category.Id;
            Name = category.Name;
            Description = category.Description;
            Categories = categories.ToList();
        }

        public CreateCategoryModel(IEnumerable<ICategory> categories)
        {            
            Categories = categories.ToList();
        }

        private Guid _id = Guid.Empty;

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        } 

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public ICategory ParentCategory { get; set; }

        public List<ICategory> SubCategories { get; set; }

        public List<ICategory> Categories { get; set; } 
    }
}