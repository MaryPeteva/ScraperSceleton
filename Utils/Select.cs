namespace ScraperSceleton.Utils
{
    public class Select
    {
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
