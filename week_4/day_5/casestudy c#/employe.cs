using System;

namespace HRSystem
{
    public class Employee
    {
        // Private fields (data hiding)
        private string _fullName =" ";
        private int _age;
        private decimal _salary;

        // Readonly field for EmployeeId
        private readonly string _employeeId;

        // FullName Property
        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Full name cannot be empty.");

                _fullName = value.Trim();
            }
        }

        // Age Property
        public int Age
        {
            get => _age;
            set
            {
                if (value < 18 || value > 80)
                    throw new ArgumentException("Age must be between 18 and 80.");

                _age = value;
            }
        }

        // Salary Property
        public decimal Salary
        {
            get => _salary;
            private set
            {
                if (value < 1000)
                    throw new ArgumentException("Salary cannot be less than 1000.");

                _salary = value;
            }
        }

        // Read-only EmployeeId
        public string EmployeeId => _employeeId;

        // Constructor
        public Employee(string fullName, decimal salary, int age, string ? employeeId = null)
        {
            
            FullName = fullName;
            Age = age;
            Salary = salary;

            _employeeId = string.IsNullOrWhiteSpace(employeeId)
                ? "E" + Guid.NewGuid().ToString().Substring(0, 6)
                : employeeId;
        }

        // Method to Give Raise
        public void GiveRaise(decimal percentage)
        {
            if (percentage <= 0 || percentage > 30)
                throw new ArgumentException("Raise percentage must be between 0 and 30.");

            Salary = Salary + (Salary * percentage / 100);

            Console.WriteLine($"Salary updated successfully. New Salary: {Salary:C}");
        }

        // Method to Deduct Penalty
        public bool DeductPenalty(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Penalty must be greater than zero.");

            if (Salary - amount < 1000)
            {
                Console.WriteLine("Penalty cannot be applied. Salary cannot fall below 1000.");
                return false;
            }

            Salary -= amount;

            Console.WriteLine($"Penalty deducted. New Salary: {Salary:C}");
            return true;
        }
    }
}
