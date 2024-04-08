using RecommendAlgorithm.Models;

namespace RecommendAlgorithm.SimiliarityModel
{
    public class ItemToItemSimilarity
    {
        public string _id { get; set; }
        public Item User { get; set; }
        public Dictionary<Item, double> SimilarItem { get; set; }
    }
}
