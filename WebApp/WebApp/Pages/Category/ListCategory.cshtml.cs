using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.BL.Interface;

namespace WebApp.Pages.Category
{
    public class ListCategoryModel : PageModel
    {
        public List<ICategory> Categories { get; set; }

        public void OnGet()
        {

        }
    }
}