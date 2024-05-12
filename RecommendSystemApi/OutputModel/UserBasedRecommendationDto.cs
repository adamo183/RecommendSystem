namespace RecommendSystem.Api.OutputModel
{
    public class UserBasedRecommendationDto
    {
        public int UserId { get; set; }
        public List<string> RecommendationList { get; set; }
    }
}
