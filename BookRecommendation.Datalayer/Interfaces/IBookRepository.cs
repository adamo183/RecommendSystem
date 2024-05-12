using BookRecommendation.Datalayer.MongoModel;
using BookRecommendation.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<BookDb>> GetBookByIds(List<string> booksIds);
        public Task<BookDb> GetBookById(string bookId);
    }
}
