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
            _database.Categories.Add(model);
            _database.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
