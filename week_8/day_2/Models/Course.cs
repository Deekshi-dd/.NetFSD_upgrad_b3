namespace WebApplication14.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        // Navigation property
        public List<Student> Students { get; set; }
    }
}
