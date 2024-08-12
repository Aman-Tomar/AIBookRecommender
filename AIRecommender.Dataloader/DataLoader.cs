using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.Entities;
using CsvHelper;
using CsvHelper.Configuration;

namespace AIRecommender.Dataloader
{
    public class DataLoader : IDataLoader
    {
        // Store filepath
        string[] filePaths = new string[3] { "BX-Books.csv", "BX-Users.csv", "BX-Book-Ratings.csv" };

        // Create BookDetails object
        BookDetails bookDetails = new BookDetails();

        public BookDetails Load()
        {
            Parallel.Invoke(LoadRating, LoadBooks, LoadUsers);
            Console.WriteLine("Completed");
            return bookDetails;
        }

        private void LoadBooks()
        {
            StreamReader bookReader = new StreamReader(filePaths[0]);

            // Skip header
            bookReader.ReadLine();
            
            using (bookReader)
            {
                while (!bookReader.EndOfStream)
                {
                    string delimiter = "\";\"";
                    string[] line = bookReader.ReadLine().Split(new[] { delimiter }, StringSplitOptions.None);

                    // Console.WriteLine(line[0].Substring(1).Trim('"'));

                    bookDetails.Books.Add(new Book
                    {
                        ISBN = line[0].Substring(1).Trim('"'),
                        BookTitle = line[1],
                        BookAuthor = line[2],
                        YearOfPublication = int.Parse(line[3]),
                        Publisher = line[4],
                        ImageUrlSmall = line[5],
                        ImageUrlMedium = line[6],
                        ImageUrlLarge = line[7],
                    });
                }
            }
            Console.WriteLine("Completed books");
        }
        private void LoadUsers()       
        {
            StreamReader userReader = new StreamReader(filePaths[1]);

            // Skip header
            userReader.ReadLine();

            using (userReader)
            {
                while (!userReader.EndOfStream)
                {
                    string delimiter = "\";";
                    string[] line = userReader.ReadLine().Split(new[] { delimiter }, StringSplitOptions.None);

                    List<string> location = line[1].Substring(1).Split(',').Select(x => x.Trim(' ')).ToList();

                    var city = location.Count >= 1 ? location[0] : "";
                    var state = location.Count >= 2 ? location[1] : "";
                    var country = location.Count >= 3 ? location[2] : "";

                    if (line.Length < 3 )
                    {
                        continue;
                    }
                    bookDetails.Users.Add(new User
                    {
                        UserID = int.Parse(line[0].Substring(1)),
                        City = city,
                        State = state,
                        Country = country,
                        Age = line[2] == "NULL" ? -1 : int.Parse(line[2].Trim('"')),    
                    });
                }
            }
            Console.WriteLine("Completed users");
        }

        private void LoadRating()
        {
            StreamReader ratingReader = new StreamReader(filePaths[2]);

            // Skip header
            ratingReader.ReadLine();

            using (ratingReader)
            {
                int count = 0;
                while (!ratingReader.EndOfStream)
                {
                    string delimiter = ";";
                    string[] line = ratingReader.ReadLine().Split(delimiter);

                    bookDetails.BookUserRatingsList.Add(new BookUserRating
                    {
                        UserID = int.Parse(line[0].Trim('"')),
                        ISBN = line[1].Substring(1).Trim('"'),
                        Rating = int.Parse(line[2].Trim('"')),
                    });

                }
            }
            Console.WriteLine("Completed rating");
        }
    }
}
