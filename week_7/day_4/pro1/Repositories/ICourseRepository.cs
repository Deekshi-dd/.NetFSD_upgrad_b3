using WebApplication11.Models;

namespace WebApplication11.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course GetById(int id);
        void Add(Course course);
    }
}
