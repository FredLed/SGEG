using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.BL;
using WebApp.BL.Interface;
using WebApp.Infrastructure.SqlRepository;
using WebApp.Pages.Product;

namespace WebApp.Controller
{
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IProductRepository _productRepository;

        public IEnumerable<IProduct> Products;

        public ProductController(IServiceProvider serviceProvider)
        {
            _productRepository = (SqlProductRepository)serviceProvider.GetService(typeof(IProductRepository));

            RefreshProducts();
        }

        private void RefreshProducts()
        {
            Products = _productRepository.Products;
        }

        //GET: Product/View
        [HttpGet("Product/ViewProduct")]
        public ActionResult ViewProduct(Guid id)
        {
            RefreshProducts();
            ViewBag.Model = new ViewProductModel { Product = Products.FirstOrDefault(c => c.Id.Equals(id)) };
            return View("ViewProduct");
        }

        //GET: Product/List
        [HttpGet("Product/List")]
        public ActionResult List()
        {
            RefreshProducts();
            ViewBag.Model = new ListProductModel { Products = Products.ToList() };
            return View("ListProduct");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //GET: Product/Edit
        [HttpGet("Product/EditProduct")]
        public ActionResult EditProduct(Guid id)
        {
            RefreshProducts();
            ViewBag.Model = new CreateProductModel(Products.FirstOrDefault(c => c.Id.Equals(id)));
            return View("CreateProduct");
        }

        //Post: Product/Edit
        [HttpPost("Product/Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            try
            {
                var id = collection["productId"];
                var name = collection["name"];
                var description = collection["description"];

                var product = (Product)Products.FirstOrDefault(c => c.Id.ToString().Equals(id));

                product.Name = name;
                product.Description = description;

                if (_productRepository.SaveProduct(product))
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

        [HttpGet("Product/CreateProduct")]
        public IActionResult CreateProduct()
        {
            try
            {
                var model = new CreateProductModel();

                ViewBag.Model = model;

                return View("CreateProduct", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // POST: Product/Create
        [HttpPost("Product/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newProd = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = collection["name"],
                    Description = collection["description"],
                    Category = null,
                    CUP = string.Empty,
                    MSRP = 0,
                    CreationDate = DateTime.Now
                };

                if (_productRepository.SaveProduct(newProd))
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