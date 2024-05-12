using BookRecommendation.Client.Interfaces;
using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private IRecommendationRepository _recommendationRepository;
        public RecommendationController(IRecommendationRepository recommendationRepository) 
        {
            _recommendationRepository = recommendationRepository;
        }

        [HttpGet("user/{userId}/recommendation")]
        public async Task<ActionResult> GetUserRecommendation(int userId)
        {
            var userRecommendation = await _recommendationRepository.GetRecommendationsToUser(userId);
            return Ok(userRecommendation);
        }
    }
}
