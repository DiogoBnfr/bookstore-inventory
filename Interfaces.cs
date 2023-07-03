using Spectre.Console;
using System.Data;

namespace bookstore_system
{
    internal class Interfaces
    {
        public static void MainMenu()
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
        }

        public static void Insertion()
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
    }
}
