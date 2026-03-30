using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> products = new List<Product>()
        {
            new Product{ ProductId=1, ProductName="Laptop", Price=50000, Category="Electronics"},
            new Product{ ProductId=2, ProductName="Mobile", Price=20000, Category="Electronics"}
        };

        // SHOW ALL PRODUCTS (IMPORTANT)
        public IActionResult Index()
        {
            return View(products);
        }

        // CREATE
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                products.Add(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // EDIT
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(x => x.ProductId == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product p)
        {
            var existing = products.FirstOrDefault(x => x.ProductId == p.ProductId);

            existing.ProductName = p.ProductName;
            existing.Price = p.Price;
            existing.Category = p.Category;

            return RedirectToAction("Index");
        }

        // DELETE
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(x => x.ProductId == id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var product = products.FirstOrDefault(x => x.ProductId == id);
            products.Remove(product);

            return RedirectToAction("Index");
        }
    }
}
