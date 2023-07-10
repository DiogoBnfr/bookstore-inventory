using Spectre.Console;

namespace bookstore_system;

public class Book
{
    private string Title { get; set; }
    private string Author { get; set; }
    private int Pages { get; set; }
    private string ISBN { get; set; }
    private int PagesRead { get; set; }

    public string GetTitle() { return Title; }
    public string GetAuthor() { return Author; }
    public int GetPages() { return Pages; }
    public int GetPagesRead() { return PagesRead; }
    public string GetISBN() { return ISBN; }

    public Book(string title, string author, int pages, string isbn) 
    {
        Title = title;
        Author = author;
        Pages = pages;
        ISBN = isbn;
        PagesRead = 0;
    }

    public static Book CreateBook()
    {
        var rule = new Rule("CREATE");
        AnsiConsole.Write(rule);

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
        string percentage = Percentage(book.Pages, book.PagesRead);

        var table = new Table();

        table.AddColumns("Title", "Author", "Pages", "ISBN", "Percentage")
            .AddRow(title, author, pages, isbn, percentage)
            .Expand().Border(TableBorder.AsciiDoubleHead);

        AnsiConsole.Write(table);
    }

    public enum Filter { 
        ISBN,
        Title,
        Author,
        Pages
    }

    public static string Percentage(int totalPages, int pagesRead)
    {
        if (pagesRead > totalPages) pagesRead = totalPages;
        double percentage = (double)pagesRead / totalPages * 100;
        string displayPercentage = "";
        int loop;
        if (percentage < 10) 
        {
            loop = 0;
        }
        else if (percentage >= 10 && percentage < 100)
        {
            loop = Convert.ToInt32((percentage / 10) % 10);
        }
        else
        {
            loop = 10;
        }
        for (int i = 0; i < loop; i++)
        {
            displayPercentage += "#";
        }

        if (percentage >= 10)
        {
            return displayPercentage + " " + (int)percentage + "%";
        }
        else
        {
            return (int)percentage + "%";
        }
    }
}