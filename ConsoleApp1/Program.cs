using AIRecommender.Aggregator;
using AIRecommender.Dataloader;
using AIRecommender.Entities;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 0441662382, 087113375X, 
            // 3453073398, 
            Preference preference = new Preference { ISBN= "0836226992", MinAge=0, MaxAge=100, State= "washington" };
            
           /* Console.WriteLine($"Enter your preference");
            Console.WriteLine("ISBN");
            preference.ISBN = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Minimum Age");
            preference.MinAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Maximum Age");
            preference.MaxAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("State");
            preference.State = Convert.ToString(Console.ReadLine());

            Console.WriteLine($"Enter the number of books you want");
            int limit = Convert.ToInt32(Console.ReadLine());
*/
            var books = AIRecommdationEngine.Recommend(preference, 10);

            foreach (var book in books)
            {
                Console.WriteLine($"ISBN: {book.ISBN} \t Title: {book.BookTitle} \t Author: {book.BookAuthor}");
            }
        }
    }
}
