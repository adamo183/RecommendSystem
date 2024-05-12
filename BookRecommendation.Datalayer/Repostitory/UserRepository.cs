using AutoMapper;
using BookRecommendation.Datalayer.Interfaces;
using BookRecommendation.Datalayer.Model;
using BookRecommendation.Datalayer.MongoModel;
using BookRecommendation.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BookRecommendation.Datalayer.Repostitory
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserDb> _users;
        private readonly IMongoCollection<UserPassword> _userPassword;
        private readonly IMongoCollection<RatingDb> _ratingDb;
        private readonly IMongoCollection<BookDb> _bookDb;
        private readonly IMapper _mapper;
        public UserRepository(IOptions<MongoDBSettings> mongoDBSettings, IMapper mapper)
        {
            _mapper = mapper;
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _users = database.GetCollection<UserDb>(mongoDBSettings.Value.UserCollection);
            _userPassword = database.GetCollection<UserPassword>(mongoDBSettings.Value.UserPasswordCollection);
            _ratingDb = database.GetCollection<RatingDb>(mongoDBSettings.Value.RatingsCollection);
            _bookDb = database.GetCollection<BookDb>(mongoDBSettings.Value.BooksCollection);
        }

        public async Task<List<User>> GetUsers()
        {
            FilterDefinition<UserDb> filter = Builders<UserDb>.Filter.Lte(x => x.UserId, 100);
            var usersDbCollection = await _users.Find(filter).ToListAsync();

            if (usersDbCollection.Count > 0)
            {
                var usersDto = _mapper.Map<List<User>>(usersDbCollection);
                return usersDto;
            }
            return new List<User>();
        }

        public async Task<UserPassword?> FindUserByLoginAndPassword(string login, string password)
        {
            FilterDefinition<UserPassword> filter = Builders<UserPassword>.Filter.Eq(x => x.Login, login) & Builders<UserPassword>.Filter.Eq(x => x.Password, password);
            var user = await (await _userPassword.FindAsync(filter)).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserDb> FindUserDetailById(int id)
        {
            FilterDefinition<UserDb> filter = Builders<UserDb>.Filter.Eq(x => x.UserId, id);
            var user = await (await _users.FindAsync(filter)).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<BookDb>> FindUserBookByUserId(int id)
        {
            FilterDefinition<RatingDb> filter = Builders<RatingDb>.Filter.Eq(x => x.UserId, id);
            var ratingCollection = (await _ratingDb.FindAsync(filter)).ToList().Select(x => x.BookId);

            FilterDefinition<BookDb> filterBook = Builders<BookDb>.Filter.In(x => x.BookId, ratingCollection);
            var bookCollection = await(await _bookDb.FindAsync<BookDb>(filterBook)).ToListAsync();
            return bookCollection;
        }
    }
}
