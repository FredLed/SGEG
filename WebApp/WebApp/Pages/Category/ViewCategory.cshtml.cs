using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.BL.Interface;

namespace WebApp.Pages.Category
{
    public class ViewCategoryModel : PageModel
    {
        public ICategory Category { get; set; }

        public void OnGet()
        {

        }
    }
}