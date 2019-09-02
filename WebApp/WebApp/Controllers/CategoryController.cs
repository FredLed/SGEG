using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.BL;
using WebApp.BL.Interface;
using WebApp.Infrastructure.SqlRepository;
using WebApp.Pages.Category;

namespace WebApp.Controller
{
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private ICategoryRepository _categoryRepository;

        public IEnumerable<ICategory> Categories;

        public CategoryController(IServiceProvider serviceProvider)
        {
            _categoryRepository = (SqlCategoryRepository)serviceProvider.GetService(typeof(ICategoryRepository));

            RefreshCategories();
        }

        private void RefreshCategories()
        {
            Categories = _categoryRepository.Caterogies;
        }

        //GET: Category/List
        [HttpGet("Category/List")]
        public ActionResult List()
        {
            RefreshCategories();
            ViewBag.Model = new ListCategoryModel { Categories = Categories.ToList() };
            return View("ListCategory");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost("Category/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newCat = new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = collection["name"],
                    Description = collection["description"],
                    ParentCategory = null,
                    SubCategories = null
                };

                if (_categoryRepository.SaveCategory(newCat))
                {

                    return Redirect(Url.Action("List"));
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}