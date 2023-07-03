using Spectre.Console;
namespace bookstore_system
{
    internal class Interfaces
    {
        public void Insertion()
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
                Console.Clear();
            }
        }
    }
}
