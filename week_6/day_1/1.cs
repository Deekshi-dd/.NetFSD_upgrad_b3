using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Application Started...\n");

       
        Task log1 = WriteLogAsync("User logged in");
        Task log2 = WriteLogAsync("File uploaded");
        Task log3 = WriteLogAsync("Error occurred");
        Task log4 = WriteLogAsync("User logged out");

        Console.WriteLine("Logging in progress...\n");

    
        await Task.WhenAll(log1, log2, log3, log4);

        Console.WriteLine("\nAll logs written successfully!");
        Console.WriteLine("Application Finished.");
    }

  
    static async Task WriteLogAsync(string message)
    {
        Console.WriteLine($"Start writing log: {message}");

        // Simulate file writing delay
        await Task.Delay(2000);

        Console.WriteLine($"Finished writing log: {message}");
    }
}
