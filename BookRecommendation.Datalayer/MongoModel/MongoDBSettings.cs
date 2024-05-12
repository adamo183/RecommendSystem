using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.MongoModel
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UserCollection { get; set; } = null!;
        public string UserPasswordCollection { get; set; } = null!;
        public string BooksCollection { get; set; } = null!;
        public string RatingsCollection { get; set; } = null!;
        public string UserToUserCollection { get; set; } = null!;
    }
}
