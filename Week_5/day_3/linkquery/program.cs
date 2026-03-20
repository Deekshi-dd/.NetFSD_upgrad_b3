using System;
using System.Linq;

namespace LinqCodeTemplate
{
    internal class Program
    {
        static void Main()
        {
            Product product = new Product();
            var products = product.GetProducts();

            int choice;

            do
            {
                Console.WriteLine("\n===== LINQ MENU =====");
                Console.WriteLine("1. FMCG Products");
                Console.WriteLine("2. Grain Products");
                Console.WriteLine("3. Sort by Code");
                Console.WriteLine("4. Sort by Category");
                Console.WriteLine("5. Sort by Price (Asc)");
                Console.WriteLine("6. Sort by Price (Desc)");
                Console.WriteLine("7. Group by Category");
                Console.WriteLine("8. Group by Price");
                Console.WriteLine("9. Highest Price FMCG");
                Console.WriteLine("10. Count Total");
                Console.WriteLine("11. Count FMCG");
                Console.WriteLine("12. Max Price");
                Console.WriteLine("13. Min Price");
                Console.WriteLine("14. All < 30?");
                Console.WriteLine("15. Any < 30?");
                Console.WriteLine("16. Exit");
                Console.Write("Enter choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Display(products.Where(p => p.ProCategory == "FMCG"));
                        break;

                    case 2:
                        Display(products.Where(p => p.ProCategory == "Grain"));
                        break;

                    case 3:
                        Display(products.OrderBy(p => p.ProCode));
                        break;

                    case 4:
                        Display(products.OrderBy(p => p.ProCategory));
                        break;

                    case 5:
                        Display(products.OrderBy(p => p.ProMrp));
                        break;

                    case 6:
                        Display(products.OrderByDescending(p => p.ProMrp));
                        break;

                    case 7:
                        var groupCat = products.GroupBy(p => p.ProCategory);
                        foreach (var g in groupCat)
                        {
                            Console.WriteLine($"\nCategory: {g.Key}");
                            Display(g);
                        }
                        break;

                    case 8:
                        var groupPrice = products.GroupBy(p => p.ProMrp);
                        foreach (var g in groupPrice)
                        {
                            Console.WriteLine($"\nPrice: {g.Key}");
                            Display(g);
                        }
                        break;

                    case 9:
                        var high = products
                            .Where(p => p.ProCategory == "FMCG")
                            .OrderByDescending(p => p.ProMrp)
                            .FirstOrDefault();

                            Console.WriteLine(high != null ? $"{high.ProCode}\t{high.ProName}\t{high.ProMrp}" : "No FMCG products found.");
                            break;

                    case 10:
                        Console.WriteLine("Total Products: " + products.Count());
                        break;

                    case 11:
                        Console.WriteLine("FMCG Count: " + products.Count(p => p.ProCategory == "FMCG"));
                        break;

                    case 12:
                        Console.WriteLine("Max Price: " + products.Max(p => p.ProMrp));
                        break;

                    case 13:
                        Console.WriteLine("Min Price: " + products.Min(p => p.ProMrp));
                        break;

                    case 14:
                        Console.WriteLine("All < 30: " + products.All(p => p.ProMrp < 30));
                        break;

                    case 15:
                        Console.WriteLine("Any < 30: " + products.Any(p => p.ProMrp < 30));
                        break;

                    case 16:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

            } while (choice != 16);
        }

        // Common display method
        static void Display(IEnumerable<Product> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
            }
        }
    }
}
