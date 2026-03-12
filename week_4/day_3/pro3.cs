namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            double salary;
            int exp;
            int bonus_per;
            double bonus;
            double final_salary;


            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Salary: ");
            salary = double.Parse(Console.ReadLine());

            Console.Write("Enter Experience: ");
            exp = int.Parse(Console.ReadLine());

            if(exp > 5)
            {
                bonus_per = 15;
            }else if (exp >= 2 && exp <= 5)
            {
                bonus_per = 10;
            }
            else
            {
                bonus_per = 5;
            }

            bonus = salary * bonus_per / 100;

            final_salary = salary + bonus;

            Console.WriteLine("----- Result -----");
            Console.WriteLine($"Employee: {name}");
            Console.WriteLine($"Bonus: {bonus}");
            Console.WriteLine($"Final salary: {final_salary}");
        }
    }
}
