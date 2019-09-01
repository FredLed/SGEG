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
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public ICategory ParentCategory { get; set; }

        public List<ICategory> SubCategories { get; set; }
    }
}