using CsvHelper;
using MongoDB.Driver;
using RecommendAlgorithm.Models;
using RecommendSystem.ReadFile;
using RecommendSystemApi.Interfaces;

namespace RecommendSystemApi.Services
{
    public class DataServices : IDataServices
    {
        public DataServices() 
        {
            LoadDataFromCsv();
        }

        CsvManager csvReader = new CsvManager();
        public List<Rating> ratings { get; set; }
        public List<User> users { get; set; }
        public List<Item> items { get; set; }

        public List<Item> GetItems() => items;

        public List<Rating> GetRatings() => ratings;

        public List<User> GetUsers() => users;

        public void LoadDataFromCsv()
        {
            ratings = csvReader.ReadFromCsv<Rating>("Data/Ratings.csv");
            users = csvReader.ReadFromCsv<User>("Data/Users.csv");
            items = csvReader.ReadFromCsv<Item>("Data/Books.csv");
            //data preprocessing
            ratings = ratings.Where(x => x.RatingScore > 0 && x.User < 2000).ToList();
            var usersWithRatings = ratings.Select(x => x.User).Distinct().ToList();
            var itemsWithRatings = ratings.Select(x => x.Item).Distinct().ToList();

            users = users.Where(x => usersWithRatings.Contains(x.UserId)).ToList();
            items = items.Where(x => itemsWithRatings.Contains(x.ItemName)).ToList();

            var basedRating = ratings.ToList();

            foreach (var user in users)
            {
                foreach (var item in items)
                {
                    if (basedRating.FirstOrDefault(x => x.Item == item.ItemName && x.User == user.UserId) == null)
                    {
                        ratings.Add(new Rating() { Item = item.ItemName, User = user.UserId, RatingScore = 0 });
                    }
                }
            }
        }
    }
}
