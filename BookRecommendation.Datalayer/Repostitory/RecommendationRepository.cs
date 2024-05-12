﻿using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Shared.Models;
using MongoDB.Bson.IO;

namespace BookRecommendation.Datalayer.Repostitory
{
    public class RecommendationRepository : IRecommendationRepository
    {
        public HttpClient HttpClient { get; set; }

        public RecommendationRepository(HttpClient httpClient) 
        {
            HttpClient = httpClient;
        }

        public async Task<List<RecommendationDto>> GetRecommendationsToUser(int userId)
        {
            var url = $"http://localhost:5281/api/recommendation/user/{userId}/itemToGet/20";
            var response = await HttpClient.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();
            var parsedObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RecommendationDto>>(responseString);
            return parsedObject;
        }
    }
}
