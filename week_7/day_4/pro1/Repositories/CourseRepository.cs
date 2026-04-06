using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context) { _context = context; }

        public List<Course> GetAll() => _context.Courses.ToList();
        public Course GetById(int id) => _context.Courses.Find(id);
        public void Add(Course course) { _context.Courses.Add(course); _context.SaveChanges(); }
    }
}
