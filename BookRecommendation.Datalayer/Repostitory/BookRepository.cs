using AutoMapper;
using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Datalayer.MongoModel;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Repostitory
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<BookDb> _bookDb;
        private readonly IMapper _mapper;

        public BookRepository(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper)
        {
            _mapper = mapper;
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _bookDb = database.GetCollection<BookDb>(mongoDBSettings.Value.BooksCollection);
        }

        public async Task<List<BookDb>> GetBookByIds(List<string> booksIds)
        {
            FilterDefinition<BookDb> filterBook = Builders<BookDb>.Filter.In(x => x.BookId, booksIds);
            var bookCollection = await(await _bookDb.FindAsync<BookDb>(filterBook)).ToListAsync();
            return bookCollection;
        }

        public async Task<BookDb> GetBookById(string bookId)
        {
            FilterDefinition<BookDb> filterBook = Builders<BookDb>.Filter.Eq(x => x.BookId, bookId);
            var bookCollection = await (await _bookDb.FindAsync<BookDb>(filterBook)).FirstOrDefaultAsync();
            return bookCollection;
        }
    }
}
