using System.Diagnostics.Metrics;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num1;
            int num2;
            char op;
            int result;

            Console.Write("Enter First Number: ");
            num1 = int.Parse(Console.ReadLine());


            Console.Write("Enter Second Number: ");
            num2 = int.Parse(Console.ReadLine());


            Console.Write("Enter Opeartor: ");
            op = char.Parse(Console.ReadLine());

            switch (op)
            {
                case '+':
                    result = num1 + num2;
                    break;

                case '-':
                    result = num1 - num2;
                    break;
                case '*':
                    result = num1 * num2;
                    break;
                case '/':
                    result = num1 / num2;
                    break;
                default:
                    Console.WriteLine("Enter valid Operator");
                    return;

            }
            Console.WriteLine("---- Result ----");
            Console.WriteLine($"Result: {result}");
        }
    }
}
