using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.BL.Interface;
using WebApp.Pages.Category;

namespace WebApp.Controller
{
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryModel model)
        {
            return Content($"Hello {model.Name}");
        }
    }
}