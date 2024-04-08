namespace RecommendAlgorithm.Interfaces
{
    public interface IVectorComparer
    {
        double CompareVector(double[] ratingVector, double[] secondRatingVector);
    }
}
