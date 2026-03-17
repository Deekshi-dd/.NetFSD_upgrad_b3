using System;

// 1. Custom Exception Class
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message)
    {
    }
}

// 2. BankAccount Class
class BankAccount
{
    private double balance;

    public BankAccount(double balance)
    {
        this.balance = balance;
    }

    public void Withdraw(double amount)
    {
        // 3. Throw custom exception if insufficient balance
        if (amount > balance)
        {
            throw new InsufficientBalanceException("Withdrawal amount exceeds available balance");
        }

        balance -= amount;
        Console.WriteLine("Withdrawal successful!");
        Console.WriteLine("Remaining Balance: " + balance);
    }
}

// 3. Main Program
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Balance: ");
        double balance = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Withdrawal Amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        BankAccount account = new BankAccount(balance);

        try
        {
            account.Withdraw(amount);
        }
        catch (InsufficientBalanceException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected Error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Transaction completed.");
        }

        Console.WriteLine("Program continues execution...");
    }
}
