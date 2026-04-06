using WebApplication11.Models;

namespace WebApplication11.Services
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }
}
