using AutoMapper;
using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUser()
        {
            var allUsers = await _userRepository.GetUsers();
            return Ok(allUsers);
        }

        [HttpGet("{userId}/books")]
        public async Task<ActionResult> GetUserBooks(int userId)
        {
            var userBook = await _userRepository.FindUserBookByUserId(userId);
            var booksDto = _mapper.Map<List<BookDto>>(userBook);
            return Ok(booksDto);
        }
    }
}
