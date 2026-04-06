using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;

namespace WebApplication11.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // READ - List all movies
        public IActionResult Index()
        {
            var movies = _movieService.GetAllMovies();
            return View(movies);
        }

        // READ - View single movie
        public IActionResult Details(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // CREATE - Show form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - Save to DB
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
                return View(movie);

            _movieService.AddMovie(movie);
            return RedirectToAction("Index");
        }

        // UPDATE - Show form with existing data
        public IActionResult Edit(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // UPDATE - Save changes
        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (!ModelState.IsValid)
                return View(movie);

            _movieService.UpdateMovie(movie);
            return RedirectToAction("Index");
        }

        // DELETE - Show confirmation
        public IActionResult Delete(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // DELETE - Remove from DB
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _movieService.DeleteMovie(id);
            return RedirectToAction("Index");
        }
    }
}
