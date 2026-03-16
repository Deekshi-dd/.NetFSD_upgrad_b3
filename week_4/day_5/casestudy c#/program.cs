using System;
using HRSystem;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine()!);

            Console.Write("Enter Starting Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine()!);

            Employee emp = new Employee(name, salary, age);

            Console.WriteLine("\nEmployee Created Successfully!");
            Console.WriteLine($"Employee ID: {emp.EmployeeId}");
            Console.WriteLine($"Name: {emp.FullName}");
            Console.WriteLine($"Age: {emp.Age}");
            Console.WriteLine($"Salary: {emp.Salary}");

            while (true)
            {
                Console.WriteLine("\n------ HR MENU ------");
                Console.WriteLine("1. View Employee Details");
                Console.WriteLine("2. Give Raise");
                Console.WriteLine("3. Deduct Penalty");
                Console.WriteLine("4. Change Name");
                Console.WriteLine("5. Exit");
                Console.Write("Choose option: ");

                int choice = int.Parse(Console.ReadLine()!);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nEmployee Details:");
                        Console.WriteLine($"ID: {emp.EmployeeId}");
                        Console.WriteLine($"Name: {emp.FullName}");
                        Console.WriteLine($"Age: {emp.Age}");
                        Console.WriteLine($"Salary: {emp.Salary}");
                        break;

                    case 2:
                        Console.Write("Enter raise percentage: ");
                        decimal percent = decimal.Parse(Console.ReadLine()!);
                        emp.GiveRaise(percent);
                        break;

                    case 3:
                        Console.Write("Enter penalty amount: ");
                        decimal penalty = decimal.Parse(Console.ReadLine()!);
                        emp.DeductPenalty(penalty);
                        break;

                    case 4:
                        Console.Write("Enter new name: ");
                        emp.FullName = Console.ReadLine()!;
                        Console.WriteLine("Name updated successfully.");
                        break;

                    case 5:
                        Console.WriteLine("Exiting system...");
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
