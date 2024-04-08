using RecommendAlgorithm.Interfaces;

namespace RecommendSystem.VectorComparer
{
    public class RootMeanSquare : IVectorComparer
    {
        public double CompareVector(double[] ratingVector, double[] secondRatingVector)
        {
            double score = 0.0;
            for (int i = 0; i < ratingVector.Length; i++)
            {
                score += Math.Pow(ratingVector[i] - secondRatingVector[i], 2);
            }
            return -Math.Sqrt(score / ratingVector.Length);
        }
    }
}
