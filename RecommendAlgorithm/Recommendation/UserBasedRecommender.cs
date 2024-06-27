using RecommendAlgorithm.Interfaces;
using RecommendAlgorithm.Models;
using RecommendAlgorithm.SimiliarityModel;
using RecommendAlgorithm.VectorComparer;

namespace RecommendAlgorithm.Recommendation
{
    public class UserBasedRecommender : IBuildRecomendation, IGetRecommendation<Item, UserToUserSimilarity>
    {
        public UserBasedRecommender(List<Rating> userItemTable, List<User> user, List<Item> items)
        {
            UserItemTable = userItemTable;
            Users = user;
            vectorComparer = new CosineSimilarity();
            Items = items;
        }

        public UserBasedRecommender() { }

        public List<Item> Items { get; set; }
        public List<Rating> UserItemTable { get; set; }
        public List<User> Users { get; set; }
        public List<UserToUserSimilarity> UserSimilarity { get; set; }
        public IVectorComparer vectorComparer { get; set; }

        public void BuildSimilarityDictionary<User>()
        {
            UserSimilarity = new List<UserToUserSimilarity>();
            Dictionary<int, double> SimiliarUser = new Dictionary<int, double>();
            Dictionary<int, double[]> UserRating = new Dictionary<int, double[]>();
            foreach (var item in Users)
            {
                UserRating.Add(item.UserId, UserItemTable.Where(x => x.User == item.UserId).Select(x => x.RatingScore).ToArray());
            }

            foreach (var user in Users)
            {
                var basedUserRating = UserRating.FirstOrDefault(x => x.Key == user.UserId).Value;
                foreach (var userCompared in Users)
                {
                    if (user.UserId != userCompared.UserId)
                    {
                        var comparedUserRating = UserRating.FirstOrDefault(x => x.Key == userCompared.UserId).Value;
                        double score = 0;
                        if (basedUserRating.SequenceEqual(comparedUserRating))
                        {
                            score = 0;
                        }
                        else
                        { 
                            score = vectorComparer.CompareVector(basedUserRating.ToArray(), comparedUserRating.ToArray());
                        }

                        SimiliarUser.Add(userCompared.UserId, score);
                    }
                }
                var sortedDict =  from entry in SimiliarUser orderby entry.Value descending select entry;
                UserSimilarity.Add(new UserToUserSimilarity()
                {
                    User = user.UserId,
                    SimilarUser = sortedDict.Take(100).ToDictionary()
                });
                SimiliarUser = new Dictionary<int, double>();
            }
        }

        public List<Item> GetRecommendation(int userId, int numberOfRecommendToGet, UserToUserSimilarity similarity)
        {
            List<string> RecommendationKeys = new List<string>();
            var currentUserRecommend = UserItemTable.Where(x => x.User == userId && x.RatingScore > 0).Select(x => x.Item).ToList();

            if (similarity == null)
            {
                return new List<Item>();
            }

            foreach (var item in similarity.SimilarUser)
            {
                if (RecommendationKeys.Count() > numberOfRecommendToGet)
                {
                    break;
                }

                var closeUserRecommendationTemp = UserItemTable.Where(x => x.User == item.Key).ToList();

                var closeUserRecommendation = closeUserRecommendationTemp.Where(x => 
                        !currentUserRecommend.Contains(x.Item) 
                        && x.RatingScore > 7
                        && !RecommendationKeys.Contains(x.Item)).Take(3).Select(x => x.Item).ToList();
                if (RecommendationKeys.Count + closeUserRecommendation.Count() > numberOfRecommendToGet)
                {
                    RecommendationKeys.AddRange(closeUserRecommendation.Take(RecommendationKeys.Count + closeUserRecommendation.Count() - numberOfRecommendToGet));
                }
                else
                {
                    RecommendationKeys.AddRange(closeUserRecommendation);
                }
            }

            return Items.Where(x => RecommendationKeys.Contains(x.ItemName)).ToList();
        }
    }
}
