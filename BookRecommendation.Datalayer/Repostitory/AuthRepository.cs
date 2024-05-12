using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Datalayer.Model;
using BookRecommendation.Datalayer.MongoModel;
using BookRecommendation.Shared.Model;
using BookRecommendation.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Repostitory
{
    public class AuthRepository : IAuthRepository
    {
        private readonly JSONWebTokensSettings _jwtSettings;
        private IUserRepository _userRepository;
        public AuthRepository(IOptions<JSONWebTokensSettings> jwtSettings, IUserRepository userRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var userPassword = await _userRepository.FindUserByLoginAndPassword(request.Login, request.Password);
            if (userPassword == null)
            {
                return new AuthenticationResponse()
                {
                    Id = string.Empty,
                    Token = string.Empty,
                    UserName = string.Empty
                };
            }

            var userToGenerateKey = await _userRepository.FindUserDetailById(userPassword.UserId);
            JwtSecurityToken jwtSecurityToken = await GenerateToken(userToGenerateKey);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = userToGenerateKey.UserId.ToString(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = userToGenerateKey.UserName,
            };

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(UserDb user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.UserId.ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
