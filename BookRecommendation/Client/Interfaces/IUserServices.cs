using BookRecommendation.Shared.Models;

namespace BookRecommendation.Client.Interfaces
{
    public interface IUserServices
    {
        public Task<List<BookDto>> GetUserBooks(int userId);
        public Task<List<BookDto>> GetUserRecommendation(int userId);
        public Task<BookDto> GetBookById(string bookId);
    }
}
