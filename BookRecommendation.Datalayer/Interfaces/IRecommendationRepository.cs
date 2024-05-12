using BookRecommendation.Datalayer.MongoModel;
using BookRecommendation.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Interfaces
{
    public interface IRecommendationRepository
    {
        public Task<List<RecommendationDto>> GetRecommendationsToUser(int userId);
    }
}
