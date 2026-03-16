using System;

class Product
{
    private string name;
    private double price;

  
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

  
    public double Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                Console.WriteLine("Price cannot be negative.");
            }
            else
            {
                price = value;
            }
        }
    }

  
    public virtual double CalculateDiscount()
    {
        return price;
    }
}

class Electronics : Product
{
    public override double CalculateDiscount()
    {
        double discount = Price * 0.05;
        return Price - discount;
    }
}

class Clothing : Product
{
    public override double CalculateDiscount()
    {
        double discount = Price * 0.15;
        return Price - discount;
    }
}

class Program
{
    static void Main()
    {
        Electronics e = new Electronics();
        e.Name = "Laptop";
        e.Price = 20000;

        double finalPrice = e.CalculateDiscount();

        Console.WriteLine("Final Price after 5% discount = " + finalPrice);
    }
}
