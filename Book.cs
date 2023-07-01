using Spectre.Console;
using System.Runtime.InteropServices;

namespace bookstore_system;

public class Book
{
    private string Title { get; set; }
    private string Author { get; set; }
    private int Pages { get; set; }
    private string ISBN { get; set; }

    public Book(string title, int pages, string isbn)
    {
        Title = title;
        Author = "Unknown";
        Pages = pages;
        ISBN = isbn;
    }
    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
    }

    public Book(string title, string author, int pages, string isbn) 
    {
        Title = title;
        Author = author;
        Pages = pages;
        ISBN = isbn;
    }

    public static Book CreateBook()
    {
        var title = AnsiConsole.Prompt(
            new TextPrompt<string>("Title: ")
            .PromptStyle("darkorange"));

        var author = AnsiConsole.Prompt(
            new TextPrompt<string>("[gray][[optional]][/] Author: ")
            .AllowEmpty().PromptStyle("darkorange"));

        var pages = AnsiConsole.Prompt(
            new TextPrompt<int>("[gray][[optional]][/] Pages: ")
            .DefaultValue(0).PromptStyle("darkorange"));

        var isbn = AnsiConsole.Prompt(
            new TextPrompt<string>("ISBN:")
            .PromptStyle("darkorange"));

        return new Book(title, author, pages, isbn);
    }

    public static void DisplayBookInformation(Book book)
    {
        string title = book.Title;
        string author = book.Author;
        string pages = Convert.ToString(book.Pages);
        string isbn = book.ISBN;

        var table = new Table();

        table.AddColumns("Title", "Author", "Pages", "ISBN");
        table.AddRow(title, author, pages, isbn);

        AnsiConsole.Write(table);
    }
}