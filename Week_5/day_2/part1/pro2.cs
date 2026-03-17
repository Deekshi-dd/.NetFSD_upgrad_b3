using System;

class Calculator
{
    // Method to perform division
    public void Divide(int numerator, int denominator)
    {
        try
        {
            int result = numerator / denominator;
            Console.WriteLine("Result: " + result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: Cannot divide by zero");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Operation completed safely");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calc = new Calculator();

        try
        {
            Console.Write("Enter Numerator: ");
            int num = int.Parse(Console.ReadLine());

            Console.Write("Enter Denominator: ");
            int den = int.Parse(Console.ReadLine());

            calc.Divide(num, den);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input! Please enter integers only.");
        }
        finally
        {
            Console.WriteLine("Program continues execution...");
        }
    }
}
