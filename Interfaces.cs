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
                    .AddChoices("Yes", "No"));
                if (input == "Yes")
                {
                    HandlerDB.Insert(book); break;
                }
            }
            Console.Clear();
        }

        public static void Update() 
        { 

        }
    }
}
