using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.MongoModel
{
    public class BookDb
    {
        [BsonElement("_id")]
        public ObjectId _id { get; set; }
        [BsonElement("bookId")]
        public string BookId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("author")]
        public string Author { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("publisher")]
        public string Publlisher { get; set; }
        [BsonElement("url1")]
        public string Url1 { get; set; }
        [BsonElement("url2")]
        public string Url2 { get; set; }
        [BsonElement("url3")]
        public string Url3 { get; set; }
    }
}
