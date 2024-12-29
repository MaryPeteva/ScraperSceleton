namespace ScraperSceleton.Utils
{
    public class Select
    {
        //returns the URL of selected category if no valid category is selected returns the main URL and the extraction of books will be for the whole site not just one category
        public string SelectCategory(Dictionary<string, string> categoriesList) 
        {
            Console.WriteLine("Select a category:");
            string category = Console.ReadLine().ToLower();
            string catURL = string.Empty;
            if (categoriesList.ContainsKey(category))
            {
                catURL = categoriesList[category];
            }
            else
            {
                catURL = "https://books.toscrape.com/"; //to read all books
            }

            return catURL;
        }
    }
}
