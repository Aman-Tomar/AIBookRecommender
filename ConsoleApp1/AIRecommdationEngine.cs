using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.Aggregator;
using AIRecommender.CoreEngine;
using AIRecommender.Dataloader;
using AIRecommender.Entities;

namespace ConsoleApp1
{
    public class AIRecommdationEngine
    {
        static IRatingsAggregator aggregator;
        static IRecommender recommender;
        static IDataLoader dataLoader;

        static AIRecommdationEngine() {
            aggregator = new RatingsAggregator();
            recommender = new PearsonRecommender();
            dataLoader = new DataLoader();
        }
        public static List<Book> Recommend(Preference preference, int limit)
        {   
            var bookDetails = dataLoader.Load();
            var bookRatingList = aggregator.Aggregate(bookDetails, preference);

            Console.WriteLine(bookRatingList.Count);

            if (!bookRatingList.TryGetValue(preference.ISBN, out var baseData))
            {
                Console.WriteLine($"ISBN {preference.ISBN} not found in the filtered data.");
                return new List<Book>();
            }

            Dictionary<string, double> bookPCC = new Dictionary<string, double>();

            foreach (var kvp in bookRatingList)
            {
                if (kvp.Key == preference.ISBN) continue;

                var correlation = recommender.GetCorellation(baseData.ToArray(), kvp.Value.ToArray());  
                bookPCC[kvp.Key] = correlation;
            }

            var topBooks = bookPCC.OrderByDescending(kvp => kvp.Key)
                           .Take(limit)
                           .Select(kvp => bookDetails.Books.FirstOrDefault(book => book.ISBN == kvp.Key))
                           .Where(book => book != null)
                           .ToList();

            foreach (var key in bookPCC.Keys)
            {
                Console.WriteLine($"Key: {key} \t\t Value: {bookPCC[key]}");
            }

            return topBooks;
        }
    }
}
