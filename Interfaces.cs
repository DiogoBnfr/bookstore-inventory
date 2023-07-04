using Spectre.Console;
using System.Data;

namespace bookstore_system
{
    internal class Interfaces
    {
        public static void ShowTable(DataTable dataTable)
        {
            var table = new Table();

            table.AddColumns("ISBN", "Title", "Author", "Pages").Expand().Border(TableBorder.AsciiDoubleHead);

            foreach (DataRow row in dataTable.Rows)
            {
                table.AddRow(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString());
            }
            AnsiConsole.Write(table);
        }

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
                    HandlerDB.Insert(book); 
                    break;
                }
            }
            Console.Clear();
        }

        public static void Update() 
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

                HandlerDB.Update(ISBN, newTitle, newAuthor, newPages); 
                break;
            }
        }
    }
}
