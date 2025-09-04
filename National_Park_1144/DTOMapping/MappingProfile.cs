using AutoMapper;
using National_Park_1144.Model;
using National_Park_1144.Model.DTO;

namespace National_Park_1144.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalPark, NpDTO>().ReverseMap();
            CreateMap<Trail, TrailDTO>().ReverseMap();
        }
    }
}
