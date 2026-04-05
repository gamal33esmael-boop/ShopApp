using ECommerceApp.Models;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _repo;

        public CategoryController(IRepository<Category> repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var categories = _repo.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(category);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _repo.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(category);
                _repo.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _repo.GetById(id);
            if (category == null) return NotFound();
            _repo.Delete(id);
            _repo.Save();
            return RedirectToAction("Index");
        }
    }
}