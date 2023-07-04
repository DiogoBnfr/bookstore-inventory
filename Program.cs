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

            Interfaces.ShowTable(HandlerDB.Read());

            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[] { "Create" , "Update", "Delete", "Search" })
                .HighlightStyle(Style.WithForeground(Color.Black)
                .Background(Color.White)));

            if (selection == "Create")
            {
                Interfaces.CreateBook();
            }
            if (selection == "Update")
            {
                Interfaces.Update();
            }
            Console.Clear();
        }
    }
}