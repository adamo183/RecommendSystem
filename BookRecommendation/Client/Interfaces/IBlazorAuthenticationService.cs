namespace BookRecommendation.Client.Interfaces
{
    public interface IBlazorAuthenticationService
    {
        Task<bool> Authenticate(string login, string password);
        Task Logout();
    }
}
