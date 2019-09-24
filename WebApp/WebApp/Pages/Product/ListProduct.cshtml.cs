using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.BL.Interface;

namespace WebApp.Pages.Product
{
    public class ListProductModel : PageModel
    {
        public List<IProduct> Products { get; set; }

        public void OnGet()
        {

        }
    }
}