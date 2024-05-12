using Blazored.LocalStorage;
using BookRecommendation.Client.Interfaces;
using BookRecommendation.Client.Providers;
using BookRecommendation.Shared.Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace BookRecommendation.Client.Services
{
    public class BlazorAuthenticationService : IBlazorAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        protected readonly ILocalStorageService _localStorage;

        protected IClient _client;

        public BlazorAuthenticationService(IClient client, ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            try
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest() { Login = email, Password = password };
                var authenticationResponse = await _client.AuthenticateAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.Token);
                    await _localStorage.SetItemAsync("userId", int.Parse(authenticationResponse.Id));
                    await _localStorage.SetItemAsync("userName", authenticationResponse.UserName);

                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(email);
                    _client.HttpClient.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("bearer", authenticationResponse.Token);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest() { FirstName = firstName, LastName = lastName, Email = email, UserName = userName, Password = password };
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
