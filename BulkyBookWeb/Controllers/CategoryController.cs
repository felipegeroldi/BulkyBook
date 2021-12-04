using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _database;

        public CategoryController(ApplicationDbContext database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _database.Categories.ToList();

            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if(model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }

            if(ModelState.IsValid)
            {
                _database.Categories.Add(model);
                _database.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model); 
        }

        public IActionResult Edit(int? id)
        {
            if(id is null || id == 0)
            {
                return NotFound();
            }

            var category = _database.Categories
                .Find(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }

            if (ModelState.IsValid)
            {
                _database.Categories.Update(model);
                _database.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
