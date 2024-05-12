using Microsoft.AspNetCore.Mvc;

namespace BookRecommendation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public IActionResult GetUserBook(int userId)
        {
            return Ok();
        }
    }
}
