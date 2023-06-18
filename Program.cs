namespace bookstore_system;

internal static class Program
{
    public static void Main(string[] args)
    {
        Book book = HandlerDB.CreateBook();
        Console.WriteLine(book.GetBookInformation());
    }
}