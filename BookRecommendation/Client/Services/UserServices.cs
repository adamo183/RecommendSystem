using BookRecommendation.Client.Interfaces;
using BookRecommendation.Shared.Models;
using Newtonsoft.Json;

namespace BookRecommendation.Client.Services
{
    public class UserServices : IUserServices
    {
        private IClient _httpClient;
        public UserServices(IClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookDto>> GetUserBooks(int userId)
        {
            string url = $"api/user/{userId}/books";
            var result = await _httpClient.HttpClient.GetAsync(url);
            result.EnsureSuccessStatusCode();

            var responseString = await result.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<List<BookDto>>(responseString);
            return parsedObject;
        }

        public async Task<List<BookDto>> GetUserRecommendation(int userId)
        {
            string url = $"api/recommendation/user/{userId}";
            var result = await _httpClient.HttpClient.GetAsync(url);
            result.EnsureSuccessStatusCode();

            var responseString = await result.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<List<BookDto>>(responseString);
            return parsedObject;
        }

        public async Task<BookDto> GetBookById(string bookId)
        {
            string url = $"api/book/{bookId}";
            var result = await _httpClient.HttpClient.GetAsync(url);
            result.EnsureSuccessStatusCode();

            var responseString = await result.Content.ReadAsStringAsync();
            var parsedObject = JsonConvert.DeserializeObject<BookDto>(responseString);
            return parsedObject;
        }
    }
}
