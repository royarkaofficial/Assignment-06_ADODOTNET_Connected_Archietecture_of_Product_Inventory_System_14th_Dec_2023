using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConApp_Assignment_Product_Inventory_System
{
    internal class Program
    {
        public static string conStr = "server=DESKTOP-R0LG4NK\\SQLEXPRESS;database=ProductInventoryDB;trusted_connection=true;";
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Product Inventory System");
                Console.WriteLine("1. View Product Inventory");
                Console.WriteLine("2. Add New Product");
                Console.WriteLine("3. Update Product Quantity");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ViewProductInventory();
                            break;
                        case 2:
                            AddNewProduct();
                            break;
                        case 3:
                            UpdateProductQuantity();
                            break;
                        case 4:
                            RemoveProduct();
                            break;
                        case 5:
                            Console.WriteLine("Exiting the program. Goodbye!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void ViewProductInventory()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();

                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Product Inventory:");
                        Console.WriteLine("ProductId\tProductName\tPrice\tQuantity\tMfDate\tExpDate");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ProductId"]}\t\t{reader["ProductName"]}\t\t{reader["Price"]}\t\t{reader["Quantity"]}\t\t{reader["MfDate"]}\t\t{reader["ExpDate"]}");
                        }
                    }
                }
            }
        }

        static void AddNewProduct()
        {
            Console.Write("Enter ProductId: ");
            int productid;
            int.TryParse(Console.ReadLine(), out productid);

            Console.Write("Enter ProductName: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Price: ");
            int price;
            int.TryParse(Console.ReadLine(), out price);

            Console.Write("Enter Quantity: ");
            int quantity;
            int.TryParse(Console.ReadLine(), out quantity);

            Console.Write("Enter MfDate (yyyy-mm-dd): ");
            DateTime mfDate;
            DateTime.TryParse(Console.ReadLine(), out mfDate);

            Console.Write("Enter ExpDate (yyyy-mm-dd): ");
            DateTime expDate;
            DateTime.TryParse(Console.ReadLine(), out expDate);

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();

                string query = "INSERT INTO Products (ProductId, ProductName, Price, Quantity, MfDate, ExpDate) " +
                               "VALUES (@ProductId, @ProductName, @Price, @Quantity, @MfDate, @ExpDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productid);
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@MfDate", mfDate);
                    command.Parameters.AddWithValue("@ExpDate", expDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Product added successfully.");
                    else
                        Console.WriteLine("Failed to add the product.");
                }
            }
        }
        static void UpdateProductQuantity()
        {
            Console.Write("Enter ProductId to update quantity: ");
            int productId;
            int.TryParse(Console.ReadLine(), out productId);

            Console.Write("Enter Quantity to update: ");
            int quantity;
            int.TryParse(Console.ReadLine(), out quantity);

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();

                string query = "UPDATE Products SET Quantity = @Quantity WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Quantity updated successfully.");
                    else
                        Console.WriteLine("Failed to update quantity.");
                }
            }
        }

        static void RemoveProduct()
        {
            Console.Write("Enter ProductId to remove: ");
            int productId;
            int.TryParse(Console.ReadLine(), out productId);

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();

                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Product removed successfully.");
                    else
                        Console.WriteLine("Failed to remove the product. ProductId may not exist.");
                }
            }
        }
    }
}
