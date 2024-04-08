namespace RecommendAlgorithm.Interfaces
{
    public interface IGetRecommendation<T>
    {
        List<T> GetRecommendation(int userId, int numberOfRecommendToGet);
    }
}
