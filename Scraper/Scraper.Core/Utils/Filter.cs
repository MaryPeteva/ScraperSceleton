using ScraperSceleton.Models;

namespace ScraperSceleton.Utils
{
    public class Filter
    {
        public List<Book> OrderByPriceAsc(List<Book> books) 
        {
            List<Book> sorted = books.OrderBy(book => book.Price)
                                     .ThenBy(book => book.Title)
                                     .ThenBy(book => book.Rate)
                                     .ToList();
            return sorted;
        }

        public List<Book> OrderByPriceDesc(List<Book> books)
        {
            var sorted = books.OrderByDescending(book => book.Price)
                              .ThenBy(book => book.Title)
                              .ThenBy(book => book.Rate)
                              .ToList();
            return sorted;
        }

        public List<Book> OrderByTitleAsc(List<Book> books) 
        {
            var sorted = books.OrderBy(book => book.Title)
                              .ThenBy(book => book.Rate)
                              .ThenBy(book => book.Price)
                              .ToList();
            return sorted;
        }

        public List<Book> OrderByTitleDesc(List<Book> books)
        {
            var sorted = books.OrderBy(book => book.Title)
                              .ThenBy(book => book.Rate)
                              .ThenBy(book => book.Price)
                              .ToList();
            return sorted;
        }

        public List<Book> OrderByRatingAsc(List<Book> books) 
        {
            var sortedByRating = books.OrderBy(book => book.Rate)
                              .ThenBy(book => book.Title)
                              .ThenBy(book => book.Price)
                              .ToList();
            return sortedByRating;
        }

        public List<Book> OrderByRatingDesc(List<Book> books)
        {
            var sortedByRating = books.OrderBy(book => book.Rate)
                              .ThenBy(book => book.Title)
                              .ThenBy(book => book.Price)
                              .ToList();
            return sortedByRating;
        }



    }
}
