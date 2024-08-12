using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRecommender.Entities;

namespace AIRecommender.Aggregator
{
    public interface IRatingsAggregator
    {
        public Dictionary<string, List<int>> Aggregate(BookDetails bookDetails, Preference preference);
    }
}
