using BookRecommendation.Datalayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUser()
        {
            var allUsers = await _userRepository.GetUsers();
            return Ok(allUsers);
        }
    }
}
