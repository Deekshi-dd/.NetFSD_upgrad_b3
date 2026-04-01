using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;

namespace WebApplication11.Controllers
{ 
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var movies = _service.GetMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            _service.AddMovie(movie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _service.GetMovie(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            _service.UpdateMovie(movie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _service.GetMovie(id);
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.DeleteMovie(id);
            return RedirectToAction("Index");
        }
    }
}
