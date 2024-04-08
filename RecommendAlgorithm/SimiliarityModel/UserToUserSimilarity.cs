using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using RecommendAlgorithm.Models;

namespace RecommendAlgorithm.SimiliarityModel
{
    public class UserToUserSimilarity
    {
        public ObjectId _id { get; set; }
        [BsonElement("user_id")]
        public int User { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, double> SimilarUser { get; set; }
    }
}
