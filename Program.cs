using DataLayer;
using MongoDB.Bson;
using MongoDB.Driver;
using RecommendAlgorithm.Models;
using RecommendAlgorithm.Recommendation;
using RecommendAlgorithm.SimiliarityModel;
using RecommendSystem.ReadFile;
using System.Collections.Generic;
using System.Data;
using System.Linq;

const string databaseURI = "mongodb://localhost:27017";
const string databaseName = "recommendSystem";

var csvReader = new CsvManager();
var dataset = new MongoDataRepository(databaseURI, databaseName);

//read data
var ratings = csvReader.ReadFromCsv<Rating>("Data/Ratings.csv");
var users = csvReader.ReadFromCsv<User>("Data/Users.csv");
var items = csvReader.ReadFromCsv<Item>("Data/Books.csv");
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

var userBasedRecommend = new UserBasedRecommender(ratings, users, items);

var userToUser = dataset.GetCollection<List<UserToUserSimilarity>>("userToUser");
if (userToUser != null && await userToUser.EstimatedDocumentCountAsync() > 0)
{
    userBasedRecommend.UserSimilarity = await (await userToUser.FindAsync<UserToUserSimilarity>(Builders<List<UserToUserSimilarity>>.Filter.Gte("user_id", 0))).ToListAsync();
}
else
{
    userBasedRecommend.BuildSimilarityDictionary<User>();
    dataset.AppendToCollection("userToUser", userBasedRecommend.UserSimilarity.AsEnumerable());
}


var userToRecomment = users.FirstOrDefault().UserId;
var itemsToReccoment = 20;

var recommendations = userBasedRecommend.GetRecommendation(userToRecomment, itemsToReccoment);
var t = 1;
