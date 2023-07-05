﻿using Spectre.Console;
using System.Data;

namespace bookstore_system
{
    internal class Interfaces
    {
        public static void ShowTable(DataTable dataTable)
        {
            var table = new Table();

            table.AddColumns("ISBN", "Title", "Author", "Pages").Expand().Border(TableBorder.AsciiDoubleHead);


            if (dataTable.Rows.Count >= 1)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    table.AddRow(
                        row[0].ToString(),
                        row[1].ToString(),
                        row[2].ToString(),
                        row[3].ToString());
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

                HandlerDB.Update(ISBN, newTitle, newAuthor, newPages);
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
    }

    internal class InterfaceHelpers
    {
        public static void EraseLine(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
    }
}