using Spectre.Console;
using System.Data;

namespace bookstore_system
{
    internal class Interfaces
    {
        public static void CreateBook()
        {
            while (true)
            {
                Book book = Book.CreateBook();

                Book.DisplayBookInformation(book);

                string input = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Do you confirm the entered data? ")
                    .HighlightStyle(Style.WithForeground(Color.Black)
                    .Background(Color.White))
                    .AddChoices("Yes", "No"));

                if (input == "Yes")
                {
                    HandlerDB.Insert(book);
                    break;
                }
                InterfaceHelpers.EraseLine(10);
            }
            Console.Clear();
        }

        public static void UpdateBook()
        {
            while (true)
            {
                var rule = new Spectre.Console.Rule("UPDATE");
                AnsiConsole.Write(rule);

                string ISBN = AnsiConsole.Prompt(
                    new TextPrompt<string>("ISBN: "));

                string newTitle = AnsiConsole.Prompt(
                    new TextPrompt<string>("Title: ")
                    .AllowEmpty().DefaultValue("")
                    .HideDefaultValue());

                string newAuthor = AnsiConsole.Prompt(
                    new TextPrompt<string>("Author: ")
                    .AllowEmpty().DefaultValue("")
                    .HideDefaultValue());

                int newPages = AnsiConsole.Prompt(
                    new TextPrompt<int>("Pages: ")
                    .AllowEmpty().DefaultValue(0)
                    .HideDefaultValue());

                int newPagesRead = AnsiConsole.Prompt(
                    new TextPrompt<int>("Pages Read: ")
                    .AllowEmpty().DefaultValue(0)
                    .HideDefaultValue());

                HandlerDB.Update(ISBN, newTitle, newAuthor, newPages, newPagesRead);
                break;
            }
        }

        public static void DeleteBook()
        {
            while (true)
            {
                var rule = new Spectre.Console.Rule("DELETE");
                AnsiConsole.Write(rule);

                string ISBN = AnsiConsole.Prompt(
                    new TextPrompt<string>("ISBN: "));

                string input = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("You really want to delete it [red][[this action cannot be undone]][/]? ")
                    .HighlightStyle(Style.WithForeground(Color.Black)
                    .Background(Color.White))
                    .AddChoices("Yes", "No"));

                if (input == "Yes")
                {
                    HandlerDB.Delete(ISBN);
                    break;
                }
                InterfaceHelpers.EraseLine(2);
            }
            Console.Clear();
        }

        public static void FilterBook()
        {
            var queryFilter = "";

            var filter = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[] { "ISBN", "Title", "Author", "Pages" })
                .HighlightStyle(Style.WithForeground(Color.Black)
                .Background(Color.White)).Title("Filter by: "));

            if (filter == "ISBN")
            {
                string condition = AnsiConsole.Prompt(
                    new TextPrompt<string>("Search for: "));

                queryFilter = HandlerDB.Filter(Book.Filter.ISBN, condition);

                HandlerDB.Read(queryFilter);
            }
            if (filter == "Title")
            {
                string condition = AnsiConsole.Prompt(
                    new TextPrompt<string>("Search for: "));

                queryFilter = HandlerDB.Filter(Book.Filter.Title, condition);

                HandlerDB.Read(queryFilter);
            }
            if (filter == "Author")
            {
                string condition = AnsiConsole.Prompt(
                    new TextPrompt<string>("Search for: "));

                queryFilter = HandlerDB.Filter(Book.Filter.Author, condition);

                HandlerDB.Read(queryFilter);
            }
            if (filter == "Pages")
            {
                var condition = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[] { "Greater than", "Less than", "Between" })
                .HighlightStyle(Style.WithForeground(Color.Black)
                .Background(Color.White)));

                if (condition == "Greater than")
                {
                    int pagesMin = AnsiConsole.Prompt(
                        new TextPrompt<int>("Greater than: "));

                    queryFilter = HandlerDB.Filter(Book.Filter.Pages, condition, pagesMin);
                }
                if (condition == "Less than")
                {
                    int pagesMax = AnsiConsole.Prompt(
                        new TextPrompt<int>("Less than: "));

                    queryFilter = HandlerDB.Filter(Book.Filter.Pages, condition, pagesMax);
                }
                if (condition == "Between")
                {
                    int pagesMin = AnsiConsole.Prompt(
                        new TextPrompt<int>("Greater than: "));

                    int pagesMax = AnsiConsole.Prompt(
                        new TextPrompt<int>("Less than: "));

                    queryFilter = HandlerDB.Filter(Book.Filter.Pages, condition, pagesMin, pagesMax);
                }
            }

            Console.Clear();

            InterfaceHelpers.ShowLogo();
            InterfaceHelpers.ShowTable(HandlerDB.Read(queryFilter));
            _ = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[] { "Return" })
                .HighlightStyle(Style.WithForeground(Color.Black)
                .Background(Color.White)));
        }
    }

    internal class InterfaceHelpers
    {
        public static void ShowTable(DataTable dataTable)
        {
            var table = new Table();

            table.AddColumns("ISBN", "Title", "Author", "Pages", "Percentage")
                .Expand().Border(TableBorder.AsciiDoubleHead);

                    

            if (dataTable.Rows.Count >= 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {

                    table.AddRow(
                        row[0].ToString(),
                        row[1].ToString(),
                        row[2].ToString(),
                        row[4] + "/" + row[3],
                        Book.Percentage((int)row[3], (int)row[4]));
                }
                AnsiConsole.Write(table);
            }
            else
            {
                string panelText = "You don't have any data yet!";
                var panel = new Panel("[yellow]" + panelText + "[/]")
                    .Expand().AsciiBorder()
                    .PadLeft(Console.WindowWidth / 2 - (panelText.Length / 2));
                AnsiConsole.Write(panel);
            }
        }

        public static void EraseLine(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }

        public static void ShowLogo()
        {
            if (Console.WindowWidth >= 140)
            {
                var font = FigletFont.Load("../../../fonts/alligator.flf.txt");
                AnsiConsole.Write(new FigletText(font, "bookstats").Centered().Color(Color.White));
            }
            else
            {
                AnsiConsole.Markup("[red]Console width is too short to display title! Adjust to 140 columns.[/]\n");
                Thread.Sleep(1000);
                EraseLine(1);
            }
        }
    }
}