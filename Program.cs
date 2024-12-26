using HtmlAgilityPack;
using ScraperSceleton.Utils;
public class Program
{
    
    public static void Main(string[] args)
    {
        Reader _reader = new Reader();
        Filter _filter = new Filter();
        string url = "https://books.toscrape.com/";
        //Console.WriteLine("Please provide a URL to read:");//might end up with a user input far page but it's too early to say
        //url = Console.ReadLine();
        HtmlDocument page = _reader.ReadPage(url);
        Dictionary<string, string> categories = _reader.GetCategories(page);
        Select _selector = new Select();
        string selectedCatURL = _selector.SelectCategory(categories);
        
        Dictionary<string, Dictionary<int, double>> books = new Dictionary<string, Dictionary<int, double>>();
        books = _reader.GetAllBooksFromSelectedCategory(selectedCatURL, books);
        var sortedByPrice = _filter.OrderByPriceDesc(books);
        Console.WriteLine("Books sorted by price");
        foreach (var book in sortedByPrice) 
        {
            Console.WriteLine($"{book.Key},");
            foreach (var v in book.Value) 
            {
                Console.Write($" {v} ");
            }
            Console.WriteLine();
        }

        var sortedByTitle = _filter.OrderByTitleDesc(books);
        Console.WriteLine("Books sorted by title");
        foreach (var book in sortedByTitle)
        {
            Console.WriteLine($"{book.Key},");
            foreach (var v in book.Value)
            {
                Console.Write($" {v} ");
            }
            Console.WriteLine();
        }

        var sortedByRate = _filter.OrderByRatingDesc(books);
        Console.WriteLine("Books Sorted by rate");
        foreach (var book in sortedByRate)
        {
            Console.WriteLine($"{book.Key},");
            foreach (var v in book.Value)
            {
                Console.Write($" {v} ");
            }
            Console.WriteLine();
        }



    }
}
