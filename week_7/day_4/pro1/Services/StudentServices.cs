using WebApplication11.Models;
using WebApplication11.Repositories;

namespace WebApplication11.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) { _repo = repo; }

        public List<Student> GetAllStudents() => _repo.GetAll();
        public Student GetStudentById(int id) => _repo.GetById(id);
        public void AddStudent(Student student) => _repo.Add(student);
    }
}
