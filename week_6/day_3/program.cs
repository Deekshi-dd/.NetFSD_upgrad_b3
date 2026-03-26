using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

internal class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string connStr = config.GetConnectionString("DefaultConnection");

        while (true)
        {
            Console.WriteLine("\n1.Insert  2.View  3.GetById  4.Update  5.Delete  6.Exit");
            Console.Write("Enter choice: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 6) break;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();

              
                if (choice == 2)
                {
                    da.SelectCommand = new SqlCommand("sp_GetAllProducts", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine($"{row["ProductId"]} | {row["ProductName"]} | {row["Category"]} | {row["Price"]}");
                    }
                }

               
                else if (choice == 3)
                {
                    Console.Write("Enter ID: ");
                    int id = int.Parse(Console.ReadLine());

                    da.SelectCommand = new SqlCommand("sp_GetProductById", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.SelectCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = id });

                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow r = dt.Rows[0];
                        Console.WriteLine($"{r["ProductName"]} | {r["Category"]} | {r["Price"]}");
                    }
                    else
                    {
                        Console.WriteLine("Not Found");
                    }
                }

               
                else if (choice == 1)
                {
                    da.SelectCommand = new SqlCommand("sp_GetAllProducts", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.Fill(dt);

                    DataRow row = dt.NewRow();

                    Console.Write("Name: ");
                    row["ProductName"] = Console.ReadLine();

                    Console.Write("Category: ");
                    row["Category"] = Console.ReadLine();

                    Console.Write("Price: ");
                    row["Price"] = decimal.Parse(Console.ReadLine());

                    dt.Rows.Add(row);

                    da.InsertCommand = new SqlCommand("sp_InsertProduct", conn);
                    da.InsertCommand.CommandType = CommandType.StoredProcedure;

                    da.InsertCommand.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, "ProductName"));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@Category", SqlDbType.VarChar, 50, "Category"));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 0, "Price"));

                    da.Update(dt);

                    Console.WriteLine("Inserted Successfully");
                }


                else if (choice == 4)
                {
                    da.SelectCommand = new SqlCommand("sp_GetAllProducts", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.Fill(dt);

                    Console.Write("Enter ID: ");
                    int id = int.Parse(Console.ReadLine());

                    foreach (DataRow row in dt.Rows)
                    {
                        if ((int)row["ProductId"] == id)
                        {
                            Console.Write("New Name: ");
                            row["ProductName"] = Console.ReadLine();

                            Console.Write("New Category: ");
                            row["Category"] = Console.ReadLine();

                            Console.Write("New Price: ");
                            row["Price"] = decimal.Parse(Console.ReadLine());
                        }
                    }

                    da.UpdateCommand = new SqlCommand("sp_UpdateProduct", conn);
                    da.UpdateCommand.CommandType = CommandType.StoredProcedure;

                    da.UpdateCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int, 0, "ProductId"));
                    da.UpdateCommand.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, "ProductName"));
                    da.UpdateCommand.Parameters.Add(new SqlParameter("@Category", SqlDbType.VarChar, 50, "Category"));
                    da.UpdateCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal, 0, "Price"));

                    da.Update(dt);

                    Console.WriteLine("Updated Successfully");
                }

               
                else if (choice == 5)
                {
                    da.SelectCommand = new SqlCommand("sp_GetAllProducts", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.Fill(dt);

                    Console.Write("Enter ID: ");
                    int id = int.Parse(Console.ReadLine());

                    foreach (DataRow row in dt.Rows)
                    {
                        if ((int)row["ProductId"] == id)
                        {
                            row.Delete();
                        }
                    }

                    da.DeleteCommand = new SqlCommand("sp_DeleteProduct", conn);
                    da.DeleteCommand.CommandType = CommandType.StoredProcedure;

                    da.DeleteCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int, 0, "ProductId"));

                    da.Update(dt);

                    Console.WriteLine("Deleted Successfully");
                }
            }
        }
    }
}
