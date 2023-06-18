using Spectre.Console;

namespace bookstore_system;

public class HandlerDB
{
    public static Book CreateBook()
    {
        string title = AnsiConsole.Ask<string>("Title: ");
        string author = AnsiConsole.Ask<string>("Author: ");
        AnsiConsole.Prompt(new TextPrompt<string>("Enter [green]description[/]:").AllowEmpty().PromptStyle("Red"));
        string description = AnsiConsole.Ask<string>("Description: ");
        int publication = AnsiConsole.Ask<int>("Publication: ");
        int pages = AnsiConsole.Ask<int>("Pages: ");
        double price = AnsiConsole.Ask<double>("Price: ");
        int units = AnsiConsole.Ask<int>("Units: ");
        Book.BookFormat format = AnsiConsole.Ask<Book.BookFormat>("Format: ");
        return new Book(title, description, publication, price, units, format, author, pages);
    }
}
