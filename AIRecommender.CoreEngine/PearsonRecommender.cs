using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIRecommender.CoreEngine
{
    public class PearsonRecommender : IRecommender
    {
        public double GetCorellation(int[] baseData, int[] otherData)
        {
            // Ensure both arrays have same length
            if (baseData.Length > otherData.Length)
            {
                Array.Resize(ref otherData, baseData.Length);
            }
            else
            {
                Array.Resize(ref baseData, otherData.Length);
            }

            // Handle zeros by adding 1 to both corresponding elements
            for (int i = 0; i < baseData.Length; i++)
            {
                if (baseData[i] == 0 || otherData[i] == 0)
                {
                    baseData[i] = baseData[i] < 10 ? (baseData[i] + 1) : 10; 
                    otherData[i] = otherData[i] < 10 ? (otherData[i] + 1) : 10; 
                }
            }

            int baseLen = baseData.Length;
            double baseSum= 0, otherSum = 0, baseSqSum = 0, otherSqSum = 0, productSum = 0;
            for (int i = 0; i < baseLen; i++)
            { 
                baseSum += baseData[i];
                otherSum += otherData[i];
                baseSqSum += Math.Pow(baseData[i], 2);
                otherSqSum += Math.Pow(otherData[i], 2);
                productSum += baseData[i] * otherData[i];
            }

            double num = ((baseLen * productSum) - (baseSum * otherSum));
            double denom = Math.Sqrt(((baseLen * baseSqSum) - Math.Pow(baseSum, 2)) * ((baseLen * otherSqSum) - Math.Pow(otherSum, 2)));
            return num == 0 ? 0 : (num / denom);
        }
    }
}
