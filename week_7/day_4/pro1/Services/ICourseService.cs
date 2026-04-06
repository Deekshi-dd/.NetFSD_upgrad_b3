using WebApplication11.Models;

namespace WebApplication11.Services
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        Course GetCourseById(int id);
        void AddCourse(Course course);
    }
}
