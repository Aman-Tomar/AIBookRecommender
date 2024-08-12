using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.Entities;

namespace AIRecommender.Aggregator
{
	public class RatingsAggregator : IRatingsAggregator
	{
		public Dictionary<string, List<int>> Aggregate(BookDetails bookDetails, Preference preference)
		{
			Dictionary<string, List<int>> bookRatingList = new Dictionary<string, List<int>>();

            var query = bookDetails.BookUserRatingsList
						.Join(
							bookDetails.Users,
							rating => rating.UserID,
							user => user.UserID,
							(rating, user) => new { rating, user }
						)
						.Where(x => x.user.State == preference.State &&
									x.user.Age >= preference.MinAge &&
									x.user.Age <= preference.MaxAge)
						.Select(x => new
						{
							ISBN = x.rating.ISBN,
							UserID = x.user.UserID,
							Rating = x.rating.Rating
						}
					);

			foreach (var rating in query)
			{
				if (bookRatingList.ContainsKey(rating.ISBN))
				{
					bookRatingList[rating.ISBN].Add(rating.Rating);
				}
				else
				{
					bookRatingList.Add(rating.ISBN, new List<int> { rating.Rating });
				}
			}

			return bookRatingList;
		}
	}
}
