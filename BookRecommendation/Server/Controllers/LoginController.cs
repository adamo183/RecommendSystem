using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Datalayer.Model;
using BookRecommendation.Shared.Model;
using BookRecommendation.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthRepository _authenticationRepo;
        public LoginController(IAuthRepository authenticationRepo)
        {
            _authenticationRepo = authenticationRepo;
        }

        [HttpPost("auth")]
        public async Task<ActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var result = await _authenticationRepo.AuthenticateAsync(request);
            return Ok(result);
        }
    }
}
