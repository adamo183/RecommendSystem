using RecommendAlgorithm.Models;

namespace RecommendSystemApi.Interfaces
{
    public interface IDataServices
    {
        public void LoadDataFromCsv();
        public List<Rating> GetRatings();
        public List<User> GetUsers();
        public List<Item> GetItems();
    }
}
