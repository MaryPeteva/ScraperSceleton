using HtmlAgilityPack;
using ScraperSceleton.Utils;
public class Program
{
    
    public static void Main(string[] args)
    {
        Reader _reader = new Reader();
        string url = "https://books.toscrape.com/";
        //Console.WriteLine("Please provide a URL to read:");//might end up with a user input far page but it's too early to say
        //url = Console.ReadLine();
        HtmlDocument page = _reader.ReadPage(url);
        Dictionary<string, string> categories = _reader.GetCategories(page);
        Select _selector = new Select();
        string selectedCatURL = _selector.SelectCategory(categories);
        
        Dictionary<string, Dictionary<int, double>> books = new Dictionary<string, Dictionary<int, double>>();
        books = _reader.GetAllBooksFromSelectedCategory(selectedCatURL, books);
        foreach (var book in books) 
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
