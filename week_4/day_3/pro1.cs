namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            int marks;
            string grade;

            Console.Write("Enter Name: ");
            name =Console.ReadLine();

            Console.Write("Enter Marks: ");
            marks = int.Parse(Console.ReadLine());

            if(marks > 100 || marks < 0)
            {
                Console.WriteLine("Marks should be between 0 and 100");
                return;
            }

            if(marks <= 100 && marks >= 80)
            {
                grade = "A";
            }else if(marks < 80 && marks >= 60)
            {
                grade = "B";
            }
            else if (marks < 60 && marks >= 40)
            {
                grade = "C";
            }
            else if (marks < 40 && marks >= 33)
            {
                grade = "D";
            }
            else
            {
                grade = "Fail";
            }
            Console.WriteLine("----- Result -----");
            Console.WriteLine($"Student: {name}");
            Console.WriteLine($"Grade: {grade}");
        }
    }
}
