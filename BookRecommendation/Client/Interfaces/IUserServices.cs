using BookRecommendation.Shared.Models;

namespace BookRecommendation.Client.Interfaces
{
    public interface IUserServices
    {
        public Task<List<BookDto>> GetUserBooks(int userId);
    }
}
