using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DbAccess.Data;
using DTO;

namespace DataContext.Mapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<ImmoDTO, Immo>().ReverseMap();
            CreateMap<CreateImmoDTO, Immo>().ReverseMap();
            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<ImageDTO, Image>().ReverseMap();
        }
    }
}
