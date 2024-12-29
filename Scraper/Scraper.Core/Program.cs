using HtmlAgilityPack;
using ScraperSceleton.Models;
using ScraperSceleton.Utils;
public class Program
{
    
    public static void Main(string[] args)
    {
        Reader _reader = new Reader();
        Filter _filter = new Filter();
        Select _selector = new Select();
        string url = "https://books.toscrape.com/";
        //Console.WriteLine("Please provide a URL to read:");//might end up with a user input far page but it's too early to say
        //url = Console.ReadLine();
        HtmlDocument page = _reader.ReadPage(url);
        Dictionary<string, string> categories = _reader.GetCategories(page);
        string selectedCatURL = _selector.SelectCategory(categories);
        
        List<Book> books = new List<Book>();
        books = _reader.GetAllBooksFromSelectedCategory(selectedCatURL, books);
        var sortedByPrice = _filter.OrderByPriceDesc(books);
        Console.WriteLine("Books sorted by price");
        foreach (var book in sortedByPrice) 
        {
            Console.WriteLine($"{book},");
            
        }

        var sortedByTitle = _filter.OrderByTitleDesc(books);
        Console.WriteLine("Books sorted by title");
        foreach (var book in sortedByTitle)
        {
            Console.WriteLine($"{book},");
            
        }

        var sortedByRate = _filter.OrderByRatingDesc(books);
        Console.WriteLine("Books Sorted by rate");
        foreach (var book in sortedByRate)
        {
            Console.WriteLine($"{book}");
            
        }



    }
}
