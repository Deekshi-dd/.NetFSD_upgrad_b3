using WebApplication11.Models;
using WebApplication11.Repositories;

namespace WebApplication11.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        public CourseService(ICourseRepository repo) { _repo = repo; }

        public List<Course> GetAllCourses() => _repo.GetAll();
        public Course GetCourseById(int id) => _repo.GetById(id);
        public void AddCourse(Course course) => _repo.Add(course);
    }
}
