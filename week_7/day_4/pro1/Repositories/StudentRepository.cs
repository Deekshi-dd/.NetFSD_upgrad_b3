using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) { _context = context; }

        public List<Student> GetAll() => _context.Students.Include(s => s.Course).ToList();
        public Student GetById(int id) => _context.Students.Include(s => s.Course).FirstOrDefault(s => s.StudentId == id);
        public void Add(Student student) { _context.Students.Add(student); _context.SaveChanges(); }
    }
}
