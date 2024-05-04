using AutoMapper;
using BookRecommendation.Datalayer.Model;
using BookRecommendation.Datalayer.MongoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRecommendation.Datalayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDb>().ReverseMap();
        }
    }
}
