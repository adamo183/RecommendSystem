using DataLayer;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using RecommendAlgorithm.Models;
using RecommendAlgorithm.Recommendation;
using RecommendAlgorithm.SimiliarityModel;
using RecommendSystem.ReadFile;
using RecommendSystemApi.Interfaces;

namespace RecommendSystemApi.Controllers
{
    public class RecommendController : Controller
    {
        public IDataServices DataServices { get; set; }

        CsvManager csvReader;
        MongoDataRepository dataset;
        UserBasedRecommender userBasedRecommend;
        const string databaseURI = "mongodb://localhost:27017";
        const string databaseName = "recommendSystem";

        public RecommendController(IDataServices dataServices)
        {
            DataServices = dataServices;
            csvReader = new CsvManager();
            dataset = new MongoDataRepository(databaseURI, databaseName);
            userBasedRecommend = new UserBasedRecommender(dataServices.GetRatings(), dataServices.GetUsers(), dataServices.GetItems());
        }


        [HttpGet("recommendation/user/{userId}/itemToGet/{itemToGet}")]
        public IActionResult GetRecommendationToUser(int userId, int itemToGet)
        {
            var recommendations = userBasedRecommend.GetRecommendation(userId, itemToGet);
            return Ok(recommendations);
        }

        [HttpPost("recommendation/build")]
        public IActionResult PostBuildSimulatiry()
        {
            dataset.GetCollection<List<UserToUserSimilarity>>("userToUser").DeleteMany(FilterDefinition<List<UserToUserSimilarity>>);
            userBasedRecommend.BuildSimilarityDictionary<User>();
            dataset.AppendToCollection("userToUser", userBasedRecommend.UserSimilarity.AsEnumerable());
            return Ok();
        }
    }
}
