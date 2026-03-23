using System;
using System.Threading.Tasks;

class OrderProcessing
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting order processing...\n");

        // Simulate processing an order
        await ProcessOrderAsync(orderId: 101);

        Console.WriteLine("\nOrder processing completed!");
    }

    // Orchestrates the steps in logical order
    static async Task ProcessOrderAsync(int orderId)
    {
        Console.WriteLine($"Order {orderId}: Payment verification started.");
        bool paymentVerified = await VerifyPaymentAsync(orderId);
        Console.WriteLine($"Order {orderId}: Payment verification {(paymentVerified ? "successful" : "failed")}.\n");

        if (!paymentVerified)
        {
            Console.WriteLine($"Order {orderId}: Processing stopped due to payment failure.");
            return;
        }

        Console.WriteLine($"Order {orderId}: Inventory check started.");
        bool inventoryAvailable = await CheckInventoryAsync(orderId);
        Console.WriteLine($"Order {orderId}: Inventory check {(inventoryAvailable ? "successful" : "failed")}.\n");

        if (!inventoryAvailable)
        {
            Console.WriteLine($"Order {orderId}: Processing stopped due to insufficient inventory.");
            return;
        }

        Console.WriteLine($"Order {orderId}: Confirming order...");
        await ConfirmOrderAsync(orderId);
        Console.WriteLine($"Order {orderId}: Order confirmed.\n");
    }

    // Simulate payment verification
    static async Task<bool> VerifyPaymentAsync(int orderId)
    {
        await Task.Delay(2000); // Simulate delay
        return true; // Simulate successful payment
    }

    // Simulate inventory check
    static async Task<bool> CheckInventoryAsync(int orderId)
    {
        await Task.Delay(1500); // Simulate delay
        return true; // Simulate inventory available
    }

    // Simulate order confirmation
    static async Task ConfirmOrderAsync(int orderId)
    {
        await Task.Delay(1000); // Simulate delay
    }
}
