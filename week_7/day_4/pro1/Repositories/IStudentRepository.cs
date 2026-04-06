using WebApplication11.Models;

namespace WebApplication11.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetById(int id);
        void Add(Student student);
    }
}
