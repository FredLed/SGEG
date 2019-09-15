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

        //GET: Category/View
        [HttpGet("Category/ViewCategory")]
        public ActionResult ViewCategory(Guid id)
        {
            RefreshCategories();
            ViewBag.Model = new ViewCategoryModel { Category = Categories.FirstOrDefault(c => c.Id.Equals(id)) };
            return View("ViewCategory");
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

        //GET: Category/Edit
        [HttpGet("Category/EditCategory")]
        public ActionResult EditCategory(Guid id)
        {
            RefreshCategories();
            ViewBag.Model = new CreateCategoryModel(Categories.FirstOrDefault(c => c.Id.Equals(id)), Categories);
            return View("CreateCategory");
        }

        //Post: Category/Edit
        [HttpPost("Category/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            try
            {
                var id = collection["categorieId"];
                var name = collection["name"];
                var description = collection["description"];
                //var parentCategory = null;
                //var subCategories = null;

                var category = (Category)Categories.FirstOrDefault(c => c.Id.ToString().Equals(id));

                category.Name = name;
                category.Description = description;

                if (_categoryRepository.SaveCategory(category))
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

        [HttpGet("Category/CreateCategory")]
        public IActionResult CreateCategory()
        {
            try
            {
                var model = new CreateCategoryModel(Categories);

                ViewBag.Model = model;

                return View("CreateCategory", model);
            }
            catch (Exception e)
            {
                throw;
            }
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
                    ParentCategory = Categories.FirstOrDefault(c => c.Id.ToString().Equals(collection["selectedRadio"])),
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