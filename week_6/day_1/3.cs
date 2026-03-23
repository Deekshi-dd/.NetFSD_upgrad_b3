using System;
using System.Threading.Tasks;

class ReportProcessor
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting report generation...\n");

        // Start all tasks concurrently
        Task salesTask = Task.Run(() => GenerateSalesReport());
        Task inventoryTask = Task.Run(() => GenerateInventoryReport());
        Task customerTask = Task.Run(() => GenerateCustomerReport());

        // Wait for all tasks to complete
        await Task.WhenAll(salesTask, inventoryTask, customerTask);

        Console.WriteLine("\nAll reports have been generated successfully!");
    }

    static async Task GenerateSalesReport()
    {
        Console.WriteLine("Sales Report: Started");
        await Task.Delay(3000); // Simulate processing time
        Console.WriteLine("Sales Report: Finished");
    }

    static async Task GenerateInventoryReport()
    {
        Console.WriteLine("Inventory Report: Started");
        await Task.Delay(4000); // Simulate processing time
        Console.WriteLine("Inventory Report: Finished");
    }

    static async Task GenerateCustomerReport()
    {
        Console.WriteLine("Customer Report: Started");
        await Task.Delay(2000); // Simulate processing time
        Console.WriteLine("Customer Report: Finished");
    }
}
