using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperSceleton.Utils
{
    public class Filter
    {
        public Dictionary<string, Dictionary<int, double>> OrderByPriceAsc(Dictionary<string, Dictionary<int, double>> books) 
        {
            var sorted = books.OrderBy(book => book.Value.First().Value)
                         .ToDictionary(book => book.Key, book => book.Value);
            return sorted;
        }

        public Dictionary<string, Dictionary<int, double>> OrderByPriceDesc(Dictionary<string, Dictionary<int, double>> books)
        {
            var sorted = books.OrderByDescending(book => book.Value.First().Value)
                         .ToDictionary(book => book.Key, book => book.Value);
            return sorted;
        }

        public Dictionary<string, Dictionary<int, double>> OrderByTitleAsc(Dictionary<string, Dictionary<int, double>> books) 
        {
            var sorted = books.OrderBy(book => book.Key)
                 .ToDictionary(book => book.Key, book => book.Value);
            return sorted;
        }

        public Dictionary<string, Dictionary<int, double>> OrderByTitleDesc(Dictionary<string, Dictionary<int, double>> books)
        {
            var sorted = books.OrderByDescending(book => book.Key)
                 .ToDictionary(book => book.Key, book => book.Value);
            return sorted;
        }

        public Dictionary<string, Dictionary<int, double>> OrderByRatingAsc(Dictionary<string, Dictionary<int, double>> books) 
        {
            var sortedByRating = books.OrderBy(book => book.Value.First().Key)
                          .ToDictionary(book => book.Key, book => book.Value);
            return sortedByRating;
        }

        public Dictionary<string, Dictionary<int, double>> OrderByRatingDesc(Dictionary<string, Dictionary<int, double>> books)
        {
            var sortedByRating = books.OrderByDescending(book => book.Value.First().Key)
                          .ToDictionary(book => book.Key, book => book.Value);
            return sortedByRating;
        }



    }
}
