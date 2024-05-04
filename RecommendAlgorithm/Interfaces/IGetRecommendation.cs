using RecommendAlgorithm.SimiliarityModel;

namespace RecommendAlgorithm.Interfaces
{
    public interface IGetRecommendation<T, Sim>
    {
        List<T> GetRecommendation(int userId, int numberOfRecommendToGet, Sim similarity);
    }
}
