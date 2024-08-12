using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.Aggregator
{
    public class Preference
    {
        public string ISBN { get; set; }
        public string State { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
