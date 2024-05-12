using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Shared.Models
{
    public class RecommendationDto
    {
        public int UserId { get; set; }
        public List<string> RecommendationList { get; set; }
    }
}
