using System;

class Employee
{
    // Properties
    public string Name { get; set; }
    public double BaseSalary { get; set; }

    // Virtual method
    public virtual double CalculateSalary()
    {
        return BaseSalary;
    }
}

// Manager class
class Manager : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.20);
    }
}

// Developer class
class Developer : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.10);
    }
}

class Program
{
    static void Main()
    {
        double baseSalary = 50000;

        // Runtime polymorphism using base class reference
        Employee emp1 = new Manager();
        emp1.BaseSalary = baseSalary;

        Employee emp2 = new Developer();
        emp2.BaseSalary = baseSalary;

        Console.WriteLine("Manager Salary = " + emp1.CalculateSalary());
        Console.WriteLine("Developer Salary = " + emp2.CalculateSalary());
    }
}
