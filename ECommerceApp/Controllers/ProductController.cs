using ECommerceApp.Models;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;

        public ProductController(IRepository<Product> productRepo, IRepository<Category> categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Add(product);
                _productRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Update(product);
                _productRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categoryRepo.GetAll();
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepo.GetById(id);
            if (product == null) return NotFound();
            _productRepo.Delete(id);
            _productRepo.Save();
            return RedirectToAction("Index");
        }
    }
}