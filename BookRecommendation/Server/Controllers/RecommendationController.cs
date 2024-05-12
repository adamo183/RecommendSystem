using AutoMapper;
using BookRecommendation.Client.Interfaces;
using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Datalayer.MongoModel;
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
        private IBookRepository _bookRepository;
        private IMapper _mapper;

        public RecommendationController(IRecommendationRepository recommendationRepository, IBookRepository bookRepository, IMapper mapper) 
        {
            _recommendationRepository = recommendationRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserRecommendation(int userId)
        {
            var userRecommendation = await _recommendationRepository.GetRecommendationsToUser(userId);
            if (userRecommendation == null || userRecommendation.RecommendationList.Count() == 0)
            {
                return Ok(new List<BookDto>());
            }

            List<BookDb> bookToRecommend = await _bookRepository.GetBookByIds(userRecommendation.RecommendationList);
            List<BookDto> mappedBooks = _mapper.Map<List<BookDto>>(bookToRecommend);
            return Ok(mappedBooks);
        }
    }
}
