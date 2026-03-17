using System;
using System.Collections.Generic;

namespace StudentRecordSystem
{
    // 1. Define Record
    public record Student(int RollNumber, string Name, string Course, int Marks);

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            int choice;

            do
            {
                Console.WriteLine("\n===== Student Record Management System =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student by Roll Number");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice! Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddStudents(students);
                        break;

                    case 2:
                        DisplayStudents(students);
                        break;

                    case 3:
                        SearchStudent(students);
                        break;

                    case 4:
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

            } while (choice != 4);
        }

        // 2. Add Students
        static void AddStudents(List<Student> students)
        {
            Console.Write("Enter number of students: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Invalid number!");
                return;
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEnter details for student {i + 1}:");

                int roll;
                while (true)
                {
                    Console.Write("Enter Roll Number: ");
                    if (int.TryParse(Console.ReadLine(), out roll) && roll > 0)
                        break;
                    Console.WriteLine("Invalid Roll Number!");
                }

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Course: ");
                string course = Console.ReadLine();

                int marks;
                while (true)
                {
                    Console.Write("Enter Marks (0-100): ");
                    if (int.TryParse(Console.ReadLine(), out marks) && marks >= 0 && marks <= 100)
                        break;
                    Console.WriteLine("Invalid Marks!");
                }

                students.Add(new Student(roll, name, course, marks));
            }
        }

        // 3. Display Students
        static void DisplayStudents(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No records available.");
                return;
            }

            Console.WriteLine("\nStudent Records:");
            foreach (var s in students)
            {
                Console.WriteLine($"Roll No: {s.RollNumber} | Name: {s.Name} | Course: {s.Course} | Marks: {s.Marks}");
            }
        }

        // 4. Search Student
        static void SearchStudent(List<Student> students)
        {
            Console.Write("Enter Roll Number to search: ");
            if (!int.TryParse(Console.ReadLine(), out int roll))
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            var student = students.Find(s => s.RollNumber == roll);

            Console.WriteLine("\nSearch Result:");
            if (student != null)
            {
                Console.WriteLine("Student Found:");
                Console.WriteLine($"Roll No: {student.RollNumber} | Name: {student.Name} | Course: {student.Course} | Marks: {student.Marks}");
            }
            else
            {
                Console.WriteLine("Student not found!");
            }
        }
    }
}
