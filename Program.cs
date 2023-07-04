using Spectre.Console;
using System.Data;

namespace bookstore_system;

internal static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            var font = FigletFont.Load("../../../fonts/alligator.flf.txt");

            AnsiConsole.Write(
                new FigletText(font, "bookstats")
                .Centered().Color(Color.White));

            var table = new Table();

            table.AddColumns("ISBN", "Title", "Author", "Pages").Expand().Border(TableBorder.AsciiDoubleHead);

            DataTable dataTable = HandlerDB.Read();

            foreach (DataRow row in dataTable.Rows)
            {
                table.AddRow(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
            }
            AnsiConsole.Write(table);

            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[] { "Create" , "Update", "Delete", "Search" })
                .HighlightStyle(Style.WithForeground(Color.Black)
                .Background(Color.White)));

            if (selection == "Create")
            {
                Interfaces.CreateBook();
            }
            Console.Clear();
        }
    }
}