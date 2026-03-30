using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
  
        List<Product> products = new List<Product>()
        {
             new Product { Id = 1, Name = "Laptop", Price = 50000, Category = "Electronics" },
             new Product { Id = 2, Name = "Mobile", Price = 20000, Category = "Electronics" },
             new Product { Id = 3, Name = "Shoes", Price = 3000, Category = "Fashion" }
       };

      
        public IActionResult Index()
        {
            return View(products);
        }

       
        public IActionResult Details(int id)
        {
           
            Product product = products.FirstOrDefault(products => products.Id == id);

            return View(product);
        }
    }
}
