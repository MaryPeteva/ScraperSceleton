using HtmlAgilityPack;
namespace ScraperSceleton.Utils
{
    //class main purpose to read the provided page
    public class Reader
    {
        private BookAttr _attr = new BookAttr();

        // reads the web page and returns HTML doc
        public HtmlDocument ReadPage(string url) 
        {
            var httpClient = new HttpClient();
            var page = httpClient.GetStringAsync(url).Result;
            var pageHTML = new HtmlDocument();
            pageHTML.LoadHtml(page);
            return pageHTML;
        }

        // gets the categories and returns a dict with category name and URL
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

        //returns all books with title, price and rating from selected category
        public Dictionary<string, Dictionary<int, double>> GetAllBooksFromSelectedCategory(string catURL, Dictionary<string, Dictionary<int, double>>  booksInCat) 
        {
           
            HtmlDocument page = ReadPage(catURL);
            var books = page.DocumentNode.SelectNodes("//article[contains(@class, 'product_pod')]");// <article class="product_pod">
            if (booksInCat == null)
            {
                booksInCat = new Dictionary<string, Dictionary<int, double>>();
            }

            
            if (books != null)
            {
                foreach (var book in books)
                {
                    Dictionary<int, double> ratePriceDict = new Dictionary<int, double>();
                    string title = _attr.GetBookTitle(book);
                    double price = _attr.GetBookPrice(book);
                    var rate = _attr.GetBookRating(book);

                    if (!booksInCat.ContainsKey(title)) 
                    {
                        ratePriceDict.Add(rate, price);
                        booksInCat.Add(title, ratePriceDict);
                    }

                }

            }
            if (IsMorePages(page)) 
            {
                catURL = NextPageUrl(page, catURL);
                GetAllBooksFromSelectedCategory(catURL, booksInCat);

            }
            return booksInCat;
        }

        //checks if there is a next page element 
        public bool IsMorePages(HtmlDocument page) 
        {
            var nextPageNode = page.DocumentNode.SelectSingleNode("//li[contains(@class, 'next')]/a");

            return nextPageNode != null;
        }

        //returns the URL for the next page
        public string NextPageUrl(HtmlDocument page, string baseCatURL) 
        {
            var nextPageNode = page.DocumentNode.SelectSingleNode("//li[contains(@class, 'next')]/a");
            var relativeHref = nextPageNode.GetAttributeValue("href", "");
            var nextPageUrl = new Uri(new Uri(baseCatURL), relativeHref).ToString();
            return nextPageUrl;
            
        } 
    }
}
