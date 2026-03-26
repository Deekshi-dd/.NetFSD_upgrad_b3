using ConsoleApp1;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;

public class ProductDAL
{
    private readonly string connStr;

    public ProductDAL()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        connStr = config.GetConnectionString("DefaultConnection");
    }

    // 🔹 Create Adapter with all commands
    private SqlDataAdapter GetAdapter(SqlConnection conn)
    {
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", conn);

        // INSERT
        adapter.InsertCommand = new SqlCommand("sp_InsertProduct", conn);
        adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
        adapter.InsertCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 100, "ProductName");
        adapter.InsertCommand.Parameters.Add("@Category", SqlDbType.VarChar, 50, "Category");

        var insPrice = adapter.InsertCommand.Parameters.Add("@Price", SqlDbType.Decimal, 0, "Price");
        insPrice.Precision = 10;
        insPrice.Scale = 2;

        // UPDATE
        adapter.UpdateCommand = new SqlCommand("sp_UpdateProduct", conn);
        adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;
        adapter.UpdateCommand.Parameters.Add("@ProductId", SqlDbType.Int, 0, "ProductId");
        adapter.UpdateCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 100, "ProductName");
        adapter.UpdateCommand.Parameters.Add("@Category", SqlDbType.VarChar, 50, "Category");

        var updPrice = adapter.UpdateCommand.Parameters.Add("@Price", SqlDbType.Decimal, 0, "Price");
        updPrice.Precision = 10;
        updPrice.Scale = 2;

        // DELETE
        adapter.DeleteCommand = new SqlCommand("sp_DeleteProduct", conn);
        adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;
        adapter.DeleteCommand.Parameters.Add("@ProductId", SqlDbType.Int, 0, "ProductId");

        return adapter;
    }

    // 🔹 GET ALL
    public DataTable GetAllProducts()
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connStr);
            using SqlDataAdapter adapter = GetAdapter(conn);

            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching products: " + ex.Message);
            return new DataTable();
        }
    }

    // 🔹 GET BY ID
    public Product GetProductById(int id)
    {
        try
        {
            DataTable table = GetAllProducts();

            foreach (DataRow row in table.Rows)
            {
                if ((int)row["ProductId"] == id)
                {
                    return new Product
                    {
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        ProductName = row["ProductName"]?.ToString(),
                        Category = row["Category"]?.ToString(),
                        Price = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0
                    };
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching product by ID: " + ex.Message);
            return null;
        }
    }

    // 🔹 INSERT (Disconnected)
    public void InsertProduct(Product product)
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connStr);
            using SqlDataAdapter adapter = GetAdapter(conn);

            DataTable table = new DataTable();
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["ProductName"] = product.ProductName ?? (object)DBNull.Value;
            row["Category"] = product.Category ?? (object)DBNull.Value;
            row["Price"] = product.Price;

            table.Rows.Add(row); // RowState = Added

            adapter.Update(table); // 🔥 sync with DB
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error inserting product: " + ex.Message);
        }
    }

    // 🔹 UPDATE (Disconnected)
    public void UpdateProduct(Product product)
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connStr);
            using SqlDataAdapter adapter = GetAdapter(conn);

            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                if ((int)row["ProductId"] == product.ProductId)
                {
                    row["ProductName"] = product.ProductName ?? (object)DBNull.Value;
                    row["Category"] = product.Category ?? (object)DBNull.Value;
                    row["Price"] = product.Price;
                }
            }

            adapter.Update(table); // 🔥 sync changes
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating product: " + ex.Message);
        }
    }

    // 🔹 DELETE (Disconnected)
    public void DeleteProduct(int id)
    {
        try
        {
            using SqlConnection conn = new SqlConnection(connStr);
            using SqlDataAdapter adapter = GetAdapter(conn);

            DataTable table = new DataTable();
            adapter.Fill(table);

            foreach (DataRow row in table.Rows)
            {
                if ((int)row["ProductId"] == id)
                {
                    row.Delete(); // RowState = Deleted
                }
            }

            adapter.Update(table); // 🔥 sync delete
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error deleting product: " + ex.Message);
        }
    }
}
