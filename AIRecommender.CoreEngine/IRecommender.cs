using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.CoreEngine
{
    public interface IRecommender
    {
        public double GetCorellation(int[] baseData, int[] otherData);
    }
}
