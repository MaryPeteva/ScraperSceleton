using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
public class Program
{
    public static void Main(string[] args)
    {

        //TODO ALL OF THIS IN THEIR OWN FILES THAT'T ATROCIOUS
        string url = "https://books.toscrape.com/";
        //Console.WriteLine("Please provide a URL to read:");//might end up with a user input far page but it's too early to say
        //url = Console.ReadLine();
        var httpClient = new HttpClient();
        var page = httpClient.GetStringAsync(url).Result;
        var pageHTML = new HtmlDocument();
        pageHTML.LoadHtml(page);

        //get all categories this will be a drop-down to select
        var categories = pageHTML.DocumentNode.SelectSingleNode("//*[@id=\"default\"]/div/div/div/aside/div[2]/ul/li/ul");
        Dictionary<string, string> categoriesList = new Dictionary<string, string>();
        if (categories != null)
        {
            var liElements = categories.SelectNodes(".//li/a");
            foreach (var li in liElements)
            {
                string categoryName = li.InnerText.Trim();
                string categoryUrl = li.GetAttributeValue("href", string.Empty);
                categoriesList.Add(categoryName, "https://books.toscrape.com/" + categoryUrl);

            }
        }
        else
        {
            Console.WriteLine("No categories found!");
        }

        //select category
        Console.WriteLine("Select a category:");
        string category = Console.ReadLine();
        string catURL = string.Empty;
        if (categoriesList.ContainsKey(category))
        {
            catURL = categoriesList[category];
        }

        //read the selected category page
        page = httpClient.GetStringAsync(catURL).Result;
        pageHTML = new HtmlDocument();
        pageHTML.LoadHtml(page);
        var books = pageHTML.DocumentNode.SelectNodes("//article[contains(@class, 'product_pod')]");// <article class="product_pod">
        Dictionary<string, Dictionary<int, double>> booksInCat = new Dictionary<string, Dictionary<int, double>>();
        string priceRegX = @"\d+(\.\d{1,2})?";
        if (books != null)
        {
            foreach (var book in books)
            {
                string title = book.SelectSingleNode(".//div[@class='image_container']/a/img").GetAttributeValue("alt", "No alt attribute");
                //Price in its own method
                var priceRaw = Regex.Match(book.SelectSingleNode(".//p[contains(@class, 'price')]").InnerHtml, priceRegX);
                double price = double.Parse(priceRaw.Value);

                //TODO: rate in its own method
                Dictionary<int, double> ratePriceDict = new Dictionary<int, double>();
                var rating = book.SelectSingleNode(".//p[contains(@class, 'star-rating')]");
                var ratingClass = rating.GetAttributeValue("class", "No class attribute");
                int rate = 0;
                switch (ratingClass) 
                {
                    case "One":
                        rate = 1;
                        break;
                    case "Two":
                        rate = 2;
                        break;
                    case "Three":
                        rate = 3;
                        break;
                    case "Four":
                        rate = 4;
                        break;
                    case "Five":
                        rate = 5;
                        break;
                }

                ratePriceDict.Add(rate, price);
                booksInCat.Add(title, ratePriceDict);

            }

        }
    }
}
