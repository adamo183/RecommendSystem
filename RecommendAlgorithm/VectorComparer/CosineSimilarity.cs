using RecommendAlgorithm.Interfaces;

namespace RecommendAlgorithm.VectorComparer
{
    public class CosineSimilarity : IVectorComparer
    {
        public double CompareVector(double[] ratingVector, double[] secondRatingVector)
        {
            double sumProduct = 0.0;
            double sumOneSquared = 0.0;
            double sumTwoSquared = 0.0;

            if (ratingVector.Length != secondRatingVector.Length)
                return 0.0;

            for (int i = 0; i < ratingVector.Length; i++)
            {
                sumProduct += ratingVector[i] * secondRatingVector[i];
                sumOneSquared += Math.Pow(ratingVector[i], 2);
                sumTwoSquared += Math.Pow(secondRatingVector[i], 2);
            }

            if (sumOneSquared == 0 || sumTwoSquared == 0)
                return 0.0;

            return sumProduct / (Math.Sqrt(sumOneSquared) * Math.Sqrt(sumTwoSquared));
        }
    }
}
