using Microsoft.VisualBasic;

namespace bookstore_system;

public class Book
{
    private string _title;
    private string _author;
    private string _description;
    private int _publication;
    private int _pages;
    private double _price;
    private int _units;
    private BookFormat _format;

    public Book(string title, int publication, double price, int units, BookFormat format, string author, int pages)
    {
        _title = title;
        _description = "This book doesn't have a description yet.";
        _publication = publication;
        _price = price;
        _units = units;
        _format = format;
        _author = author;
        _pages = pages;
    }

    public Book(string title, string description, int publication, double price, int units, BookFormat format, string author, int pages)
    {
        _title = title;
        _description = description;
        _publication = publication;
        _price = price;
        _units = units;
        _format = format;
        _author = author;
        _pages = pages;
    }

    public enum BookFormat
    {
        // ReSharper disable once InconsistentNaming
        eBook,
        Paperback,
        Hardcover
    }

    public string GetBookInformation()
    {
        string information = $"Title: {_title}\n";
        information += $"Author: {_author}\n";
        information += $"Description: {_description}\n";
        information += $"Year: {_publication}\n";
        information += $"Price: {_price}\n";
        information += $"Units: {_units}\n";
        information += $"Format: {_format}\n";
        information += $"Pages: {_pages}\n";
        return information;
    }
}
