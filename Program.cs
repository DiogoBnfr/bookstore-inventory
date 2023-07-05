using Spectre.Console;
using System.Data;

namespace bookstore_system;

internal static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            InterfaceHelpers.ShowLogo();

            InterfaceHelpers.ShowTable(HandlerDB.Read());

            var selection = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(new[] { "Create" , "Update", "Delete", "Filter", "Quit" })
                .HighlightStyle(Style.WithForeground(Color.Black)
                .Background(Color.White)));

            if (selection == "Create")
            {
                Interfaces.CreateBook();
            }
            if (selection == "Update")
            {
                Interfaces.UpdateBook();
            }
            if (selection == "Delete")
            {
                Interfaces.DeleteBook();
            }
            if (selection == "Filter")
            {
                Interfaces.FilterBook();
            }
            if (selection == "Quit")
            {
                break;
            }
            Console.Clear();
        }
    }
}