using Spectre.Console;

namespace bookstore_system;

internal static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Interfaces.MainMenu();
            Interfaces.Insertion();
        }
    }
}