using Spectre.Console;
using System.Data.SqlClient;

namespace bookstore_system;

public class HandlerDB
{
    private static string connectionString = @"Data Source=DESKTOP-BNFR\SQLEXPRESS;Database=bookstore_inventory;Trusted_Connection=True;";
    
    public static void Insert(Book book)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
        }
    }
}
