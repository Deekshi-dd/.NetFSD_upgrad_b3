using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication14.Models;

namespace WebApplication14.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly string _connStr;
        public StudentRepository(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }

        public IEnumerable<Student> GetStudentsWithCourse()
        {
            using (var con = GetConnection())
            {
                string query = @"SELECT s.*, c.CourseId, c.CourseName
                         FROM Student s
                         INNER JOIN Course c ON s.CourseId = c.CourseId";

                var result = con.Query<Student, Course, Student>(
                    query,
                    (student, course) =>
                    {
                        student.Course = course;
                        return student;
                    },
                    splitOn: "CourseId"
                );

                return result;
            }
        }

        public IEnumerable<Course> GetCoursesWithStudents()
        {
            using (var con = GetConnection())
            {
                string query = @"SELECT c.*, s.StudentId, s.StudentName, s.CourseId
                         FROM Course c
                         LEFT JOIN Student s ON c.CourseId = s.CourseId";

                var courseDict = new Dictionary<int, Course>();

                var result = con.Query<Course, Student, Course>(
                    query,
                    (course, student) =>
                    {
                        if (!courseDict.ContainsKey(course.CourseId))
                        {
                            courseDict[course.CourseId] = course;
                            course.Students = new List<Student>();
                        }

                        if (student != null)
                        {
                            courseDict[course.CourseId].Students.Add(student);
                        }

                        return course;
                    },
                    splitOn: "StudentId"
                );

                return courseDict.Values;
            }
        }
    }
}
