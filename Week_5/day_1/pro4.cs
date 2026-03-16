using System;

class Vehicle
{
  
    private string brand;
    private double rentalRatePerDay;

   
    public string Brand
    {
        get { return brand; }
        set { brand = value; }
    }

    public double RentalRatePerDay
    {
        get { return rentalRatePerDay; }
        set { rentalRatePerDay = value; }
    }

 
    public virtual double CalculateRental(int days)
    {
        if (days <= 0)
        {
            Console.WriteLine("Invalid number of rental days");
            return 0;
        }

        return rentalRatePerDay * days;
    }
}

class Car : Vehicle
{
    public override double CalculateRental(int days)
    {
        if (days <= 0)
        {
            Console.WriteLine("Invalid number of rental days");
            return 0;
        }

        double total = RentalRatePerDay * days;
        total += 500;   
        return total;
    }
}

class Bike : Vehicle
{
    public override double CalculateRental(int days)
    {
        if (days <= 0)
        {
            Console.WriteLine("Invalid number of rental days");
            return 0;
        }

        double total = RentalRatePerDay * days;
        total = total - (total * 0.05); 
        return total;
    }
}

class Program
{
    static void Main()
    {
        Vehicle vehicle;

        Console.WriteLine("Enter Vehicle Type (Car/Bike): ");
        string type = Console.ReadLine();

        Console.WriteLine("Enter Brand: ");
        string brand = Console.ReadLine();

        Console.WriteLine("Enter Rental Rate Per Day: ");
        double rate = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter Number of Days: ");
        int days = Convert.ToInt32(Console.ReadLine());

        if (type.ToLower() == "car")
        {
            vehicle = new Car();
        }
        else
        {
            vehicle = new Bike();
        }

        vehicle.Brand = brand;
        vehicle.RentalRatePerDay = rate;

        double totalRental = vehicle.CalculateRental(days);

        Console.WriteLine("Total Rental = " + totalRental);
    }
}
