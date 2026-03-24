using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static string connStr;

        static void Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connStr = config.GetConnectionString("DefaultConnection");

            while (true)
            {
                Console.WriteLine("\n1.Insert\n2.View All\n3.Update\n4.Delete\n5.Get By Id\n6.Exit");
                Console.Write("Enter choice: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 6) break;

                switch (choice)
                {
                    case 1: Insert(); break;
                    case 2: View(); break;
                    case 3: Update(); break;
                    case 4: Delete(); break;
                    case 5: GetProductById(); break;
                }
            }
        }

        
        static void Insert()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Category: ");
            string category = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            SqlCommand cmd = new SqlCommand("sp_InsertProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ProductName", name));
            cmd.Parameters.Add(new SqlParameter("@Category", category));
            cmd.Parameters.Add(new SqlParameter("@Price", price));

            cmd.ExecuteNonQuery();

            Console.WriteLine("Inserted Successfully");

            conn.Close();
        }

 
        static void View()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_GetAllProducts", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nProducts List:");
            while (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["ProductId"]} | {reader["ProductName"]} | {reader["Category"]} | {reader["Price"]}"
                );
            }

            reader.Close();
            conn.Close();
        }

        static void Update()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Category: ");
            string category = Console.ReadLine();

            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            SqlCommand cmd = new SqlCommand("sp_UpdateProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ProductId", id));
            cmd.Parameters.Add(new SqlParameter("@ProductName", name));
            cmd.Parameters.Add(new SqlParameter("@Category", category));
            cmd.Parameters.Add(new SqlParameter("@Price", price));

            cmd.ExecuteNonQuery();

            Console.WriteLine("Updated Successfully");

            conn.Close();
        }

      
        static void Delete()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            SqlCommand cmd = new SqlCommand("sp_DeleteProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ProductId", id));

            cmd.ExecuteNonQuery();

            Console.WriteLine("Deleted Successfully");

            conn.Close();
        }


        static void GetProductById()
        {
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());

            SqlCommand cmd = new SqlCommand("sp_GetProductById", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ProductId", id));

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["ProductId"]} | {reader["ProductName"]} | {reader["Category"]} | {reader["Price"]}"
                );
            }
            else
            {
                Console.WriteLine("Product not found");
            }

            reader.Close();
            conn.Close();
        }
    }
}
