using System;
using System.Diagnostics;
using System.IO;

class OrderProcessingTracing
{
    static void Main(string[] args)
    {
        // Configure trace listener to write logs to a file
        string logFilePath = "OrderProcessing.log";
        TextWriterTraceListener fileListener = new TextWriterTraceListener(File.CreateText(logFilePath));
        Trace.Listeners.Add(fileListener);
        Trace.AutoFlush = true; // Ensure logs are written immediately

        Console.WriteLine("Order processing started. Trace logs will be written to " + logFilePath);

        try
        {
            Trace.TraceInformation("Order processing started.");

            ValidateOrder();
            ProcessPayment();
            UpdateInventory();
            GenerateInvoice();

            Trace.TraceInformation("Order processing completed successfully.");
        }
        catch (Exception ex)
        {
            Trace.TraceError("Order processing failed: " + ex.Message);
        }

        Console.WriteLine("Order processing finished. Check log file for details.");
    }

    static void ValidateOrder()
    {
        Trace.WriteLine("Step 1: Validating order...");
        // Simulate validation logic
        TaskDelay(1000);
        Trace.TraceInformation("Order validation completed.");
    }

    static void ProcessPayment()
    {
        Trace.WriteLine("Step 2: Processing payment...");
        // Simulate payment logic
        TaskDelay(1500);
        Trace.TraceInformation("Payment processed successfully.");
    }

    static void UpdateInventory()
    {
        Trace.WriteLine("Step 3: Updating inventory...");
        // Simulate inventory update
        TaskDelay(1200);
        Trace.TraceInformation("Inventory updated.");
    }

    static void GenerateInvoice()
    {
        Trace.WriteLine("Step 4: Generating invoice...");
        // Simulate invoice generation
        TaskDelay(800);
        Trace.TraceInformation("Invoice generated.");
    }

    // Helper method to simulate delay
    static void TaskDelay(int milliseconds)
    {
        System.Threading.Thread.Sleep(milliseconds);
    }
}
