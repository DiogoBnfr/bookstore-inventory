using Spectre.Console;

namespace bookstore_system;

public class Book
{
    private string Title { get; set; }
    private string Author { get; set; }
    private int Pages { get; set; }
    private string ISBN { get; set; }

    public string GetTitle() 
    { 
        return Title;
    }
    public string GetAuthor() 
    { 
        return Author; 
    }
    public int GetPages() 
    {
        return Pages;
    }
    public string GetISBN() 
    { 
        return ISBN; 
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
            new TextPrompt<string>("Title: "));

        var author = AnsiConsole.Prompt(
            new TextPrompt<string>("[gray][[optional]][/] Author: ")
            .AllowEmpty().DefaultValue("Unknown"));

        var pages = AnsiConsole.Prompt(
            new TextPrompt<int>("[gray][[optional]][/] Pages: ")
            .DefaultValue(0));

        var isbn = AnsiConsole.Prompt(
            new TextPrompt<string>("ISBN:"));

        return new Book(title, author, pages, isbn);
    }

    public static void DisplayBookInformation(Book book)
    {
        string title = book.Title;
        string author = book.Author;
        string pages = Convert.ToString(book.Pages);
        string isbn = book.ISBN;

        var table = new Table();

        table.AddColumns("Title", "Author", "Pages", "ISBN")
            .AddRow(title, author, pages, isbn)
            .Expand().Border(TableBorder.Horizontal);

        AnsiConsole.Write(table);
    }
}