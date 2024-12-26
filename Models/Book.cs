using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScraperSceleton.Models
{
    public class Book
    {
        public string Title { get; set; }
        public int Rate { get; set; }
        public double Price {get; set;}
        public string Description { get; set; }

        public override string ToString() 
        {
            return $"Title: {Title}\nPrice: {Price}\nRating: {Rate}\n";
        }
    }
}
