using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honfoglalo.DAL;
using Honfoglalo.BLL;

namespace Honfoglalo.API.Mapper
{
    public class HonfoglaloProfile : Profile                                        //Mapper profilja
    {
        public HonfoglaloProfile()
        {
            CreateMap<DAL.Model.Question, BLL.DTOs.Question>().ReverseMap();
            CreateMap<DAL.Model.Field, BLL.DTOs.Field>().ReverseMap();
            CreateMap<DAL.Model.Users, BLL.DTOs.UserTable>().ReverseMap();
        }
    }
}
