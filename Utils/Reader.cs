using HtmlAgilityPack;
namespace ScraperSceleton.Utils
{
    public class Reader
    {
        private BookAttr _attr = new BookAttr();
        public HtmlDocument ReadPage(string url) 
        {
            var httpClient = new HttpClient();
            var page = httpClient.GetStringAsync(url).Result;
            var pageHTML = new HtmlDocument();
            pageHTML.LoadHtml(page);
            return pageHTML;
        }

        public Dictionary<string, string> GetCategories(HtmlDocument page) 
        {
            var categories = page.DocumentNode.SelectSingleNode("//*[@id=\"default\"]/div/div/div/aside/div[2]/ul/li/ul");
            Dictionary<string, string> categoriesList = new Dictionary<string, string>();
            if (categories != null)
            {
                var liElements = categories.SelectNodes(".//li/a");
                foreach (var li in liElements)
                {
                    string categoryName = li.InnerText.Trim();
                    string categoryUrl = li.GetAttributeValue("href", string.Empty);
                    categoriesList.Add(categoryName.ToLower(), "https://books.toscrape.com/" + categoryUrl);

                }
            }
            else
            {
                Console.WriteLine("No categories found!");
            }

            return categoriesList;
        }

        public Dictionary<string, Dictionary<int, double>> GetAllBooksFromSelectedCategory(string catURL) 
        {
            //read the selected category page
            HtmlDocument page = ReadPage(catURL);
            var books = page.DocumentNode.SelectNodes("//article[contains(@class, 'product_pod')]");// <article class="product_pod">
            Dictionary<string, Dictionary<int, double>> booksInCat = new Dictionary<string, Dictionary<int, double>>();
            
            if (books != null)
            {
                foreach (var book in books)
                {
                    Dictionary<int, double> ratePriceDict = new Dictionary<int, double>();
                    string title = _attr.GetBookTitle(book);
                    double price = _attr.GetBookPrice(book);
                    var rate = _attr.GetBookRating(book);                    
                   
                    ratePriceDict.Add(rate, price);
                    booksInCat.Add(title, ratePriceDict);

                }

            }

            return booksInCat;
        }
    }
}
