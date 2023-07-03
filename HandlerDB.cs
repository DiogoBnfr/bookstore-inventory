using Spectre.Console;
using System.Data;
using System.Data.SqlClient;

namespace bookstore_system;

public class HandlerDB
{
    private static readonly string connectionString = @"Data Source=DESKTOP-BNFR\SQLEXPRESS;Database=bookstore_inventory;Trusted_Connection=True;";
    
    public static void Insert(Book book)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            const string query = "INSERT INTO Book (ISBN, Title, Author, Pages) " +
                                 "VALUES (@ISBN,@Title, @Author, @Pages)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    string isbn = book.GetISBN();
                    string title = book.GetTitle();
                    string author = book.GetAuthor();
                    int pages = book.GetPages();

                    command.Parameters.AddWithValue("@ISBN", isbn);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@Pages", pages);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0) 
                    {
                        AnsiConsole.Markup("[green]Data inserted succefully![/]");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
                finally 
                {
                    connection.Close();
                }
            }
        }
    }

    public static DataTable Read()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Book";

            DataTable table = new DataTable();

            using (SqlCommand command = new SqlCommand(query,connection))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                    dataAdapter.Fill(table);
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                }
                finally
                {
                    connection.Close();
                }
            }

            return table;
        }
    }
}
