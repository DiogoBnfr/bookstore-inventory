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

    public static DataTable Read(string filter = "")
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Book";

            if (filter != "")
            {
                query += filter;
            }

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

    public static void Update(string ISBN, string newTitle, string newAuthor, int newPages)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            int queryFilters = 0;

            string query = "UPDATE Book SET ";
            if (newTitle != "") 
            { 
                query += "Title = @newTitle"; 
                queryFilters++;
            }
            if (newAuthor != "") 
            {
                if (queryFilters > 0) query += ", ";
                query += "Author = @newAuthor";
                queryFilters++;
            }
            if (newPages != 0) 
            {
                if (queryFilters > 0) query += ", ";
                query += "Pages = @newPages"; 
            }
            query += " WHERE ISBN = @ISBN";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@ISBN", ISBN);
                    if (newTitle != "") command.Parameters.AddWithValue("@newTitle", newTitle);
                    if (newAuthor != "") command.Parameters.AddWithValue("@newAuthor", newAuthor);
                    if (newPages != 0) command.Parameters.AddWithValue("@newPages", newPages);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        AnsiConsole.Markup("[green]Data updated succefully![/]");
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]Update failed or no rows matched the criteria.[/]");
                    }
                    Thread.Sleep(1000);
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

    public static void Delete(string ISBN)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "DELETE FROM Book WHERE ISBN = @ISBN";

            using (SqlCommand command = new SqlCommand(query, connection)) 
            {
                try
                {
                    command.Parameters.AddWithValue("@ISBN", ISBN);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0) 
                    {
                        AnsiConsole.Markup("[green]Data deleted succefully![/]");
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]Deletion failed or no rows matched the criteria.[/]");
                    }
                    Thread.Sleep(1000);
                }
                catch (SqlException ex)
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
    
    public static string Filter(Book.Filter filter, string condition, int pagesMin = 0, int pagesMax = 0)
    {
        var queryFilter  = " ";
        
        if (filter == Book.Filter.ISBN || filter == Book.Filter.Title || filter == Book.Filter.Author)
        {
            queryFilter += $"WHERE { filter } LIKE '{ condition }%'";
            Console.Write(queryFilter);
            Console.ReadLine();
        }
        else
        {
            if (condition == "Greater than")
            {
                queryFilter += $"WHERE { filter } > { pagesMin + 1 }";
            }
            if (condition == "Less than")
            {
                queryFilter += $"WHERE {filter} < { pagesMax - 1 }";
            }
            if (condition == "Between")
            {
                queryFilter += $"WHERE { filter } BETWEEN { pagesMin + 1 } AND { pagesMax - 1 }";
            }
        }

        return queryFilter;
    }
}