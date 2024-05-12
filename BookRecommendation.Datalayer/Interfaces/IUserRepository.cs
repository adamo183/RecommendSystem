using BookRecommendation.Datalayer.Model;
using BookRecommendation.Datalayer.MongoModel;
using BookRecommendation.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();
        public Task<UserPassword?> FindUserByLoginAndPassword(string login, string password);
        public Task<UserDb> FindUserDetailById(int id);
        public Task<List<BookDb>> FindUserBookByUserId(int id);
    }
}
