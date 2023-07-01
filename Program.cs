using Spectre.Console;

namespace bookstore_system;

internal static class Program
{
    public static void Main(string[] args)
    {
        Book book = Book.CreateBook();
        Book.DisplayBookInformation(book);
    }
}