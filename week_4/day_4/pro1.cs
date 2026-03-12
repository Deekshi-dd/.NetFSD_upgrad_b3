using System;

class Calculator
{
  
    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Subtract(int a, int b)
    {
        return a - b;
    }
}

class Program
{
    static void Main()
    {
        int a, b;

   
        Console.WriteLine("Enter two numbers:");
        a = Convert.ToInt32(Console.ReadLine());
        b = Convert.ToInt32(Console.ReadLine());


        Calculator calc = new Calculator();


        int addition = calc.Add(a, b);
        int subtraction = calc.Subtract(a, b);

      
        Console.WriteLine("Addition = " + addition);
        Console.WriteLine("Subtraction = " + subtraction);
    }
}
