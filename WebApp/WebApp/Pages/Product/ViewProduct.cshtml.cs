using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.BL.Interface;

namespace WebApp.Pages.Product
{
    public class ViewProductModel : PageModel
    {
        public IProduct Product { get; set; }
        public void OnGet()
        {

        }
    }
}