using System;

class BankAccount
{
  
    private int accountNumber;
    private double balance;

  
    public int AccountNumber
    {
        get { return accountNumber; }
    }

    
    public double Balance
    {
        get { return balance; }
    }

   
    public BankAccount(int accNo)
    {
        accountNumber = accNo;
        balance = 0;
    }


    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Invalid deposit amount.");
        }
        else
        {
            balance = balance + amount;
            Console.WriteLine("Deposit Successful.");
            Console.WriteLine("Current Balance = " + balance);
        }
    }


    public void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Invalid withdrawal amount.");
        }
        else if (amount > balance)
        {
            Console.WriteLine("Insufficient Balance.");
        }
        else
        {
            balance = balance - amount;
            Console.WriteLine("Withdrawal Successful.");
            Console.WriteLine("Current Balance = " + balance);
        }
    }
}

class Program
{
    static void Main()
    {
        BankAccount acc = new BankAccount(101);

 
        acc.Deposit(5000);
        acc.Withdraw(2000);
    }
}
