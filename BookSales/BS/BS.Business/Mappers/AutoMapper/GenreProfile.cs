using AutoMapper;
using BS.Model.Dtos.Genre;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreGetDto>()
                .ForMember(
                  dest => dest.GenreName,
                  opt => opt.MapFrom(src => src.GenreName == null
                                  ? ""
                                  : src.GenreName))
                .ReverseMap();

            CreateMap<GenrePostDto, Genre>();
            CreateMap<GenrePutDto, Genre>();

        }
    }
}
