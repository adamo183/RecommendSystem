using BookRecommendation.Datalayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer.Interfaces
{
    public interface IAuthRepository
    {
        public Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
    }
}
