namespace WebApplication11.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
