using ConsoleApp1;
using System;
using System.Data;

class Program
{
    static void Main()
    {
        ProductDAL dal = new ProductDAL();

        while (true)
        {
            Console.WriteLine("\n===== PRODUCT MENU =====");
            Console.WriteLine("1. Insert Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Get Product By ID");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Exit");

            Console.Write("Choose option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Insert(dal);
                    break;

                case 2:
                    ViewAll(dal);
                    break;

                case 3:
                    GetById(dal);
                    break;

                case 4:
                    Update(dal);
                    break;

                case 5:
                    Delete(dal);
                    break;

                case 6:
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void Insert(ProductDAL dal)
    {
        Product p = new Product();

        Console.Write("Name: ");
        p.ProductName = Console.ReadLine();

        Console.Write("Category: ");
        p.Category = Console.ReadLine();

        Console.Write("Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price!");
            return;
        }

        p.Price = price;

        dal.InsertProduct(p);
        Console.WriteLine("✅ Product Inserted!");
    }

    static void ViewAll(ProductDAL dal)
    {
        DataTable table = dal.GetAllProducts();

        Console.WriteLine("\n--- Product List ---");
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"{row["ProductId"]} | {row["ProductName"]} | {row["Category"]} | {row["Price"]}");
        }
    }

    static void GetById(ProductDAL dal)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        var product = dal.GetProductById(id);

        if (product != null)
        {
            Console.WriteLine($"ID: {product.ProductId}");
            Console.WriteLine($"Name: {product.ProductName}");
            Console.WriteLine($"Category: {product.Category}");
            Console.WriteLine($"Price: {product.Price}");
        }
        else
        {
            Console.WriteLine("❌ Product not found!");
        }
    }

    static void Update(ProductDAL dal)
    {
        Product p = new Product();

        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        p.ProductId = id;

        Console.Write("New Name: ");
        p.ProductName = Console.ReadLine();

        Console.Write("New Category: ");
        p.Category = Console.ReadLine();

        Console.Write("New Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price!");
            return;
        }

        p.Price = price;

        dal.UpdateProduct(p);
        Console.WriteLine("✅ Product Updated!");
    }

    static void Delete(ProductDAL dal)
    {
        Console.Write("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        dal.DeleteProduct(id);
        Console.WriteLine("✅ Product Deleted!");
    }
}
