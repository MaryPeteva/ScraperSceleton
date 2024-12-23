﻿using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

namespace ScraperSceleton.Utils
{
    public class BookAttr
    {
        public string GetBookTitle(HtmlNode book)
        {

            return WebUtility.HtmlDecode(book.SelectSingleNode(".//div[@class='image_container']/a/img").GetAttributeValue("alt", "No alt attribute"));

        }

        public int GetBookRating(HtmlNode book)
        {

            //TODO: rate in its own method
            Dictionary<int, double> ratePriceDict = new Dictionary<int, double>();
            var rating = book.SelectSingleNode(".//p[contains(@class, 'star-rating')]");
            var ratingClass = rating.GetAttributeValue("class", "No class attribute");
            int rate = RateToInt(ratingClass);
            return rate;
            
        }

        private int RateToInt(string ratingClass)
        {
            switch (ratingClass)
            {
                case "star-rating One":
                    return 1;
                case "star-rating Two":
                    return 2;
                case "star-rating Three":
                    return 3;
                case "star-rating Four":
                    return 4;                   
                case "star-rating Five":
                    return 5;
                default:
                    return 0;
            }
        }

        public double GetBookPrice(HtmlNode book)
        {
            //Price in its own method
            string priceRegX = @"\d+(\.\d{1,2})?";
            var priceRaw = Regex.Match(book.SelectSingleNode(".//p[contains(@class, 'price')]").InnerHtml, priceRegX);
            double price = double.Parse(priceRaw.Value);
            return price;

        }

    }
}

