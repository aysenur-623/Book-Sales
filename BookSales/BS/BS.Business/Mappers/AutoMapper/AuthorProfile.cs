using AutoMapper;
using BS.Model.Dtos.Author;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorGetDto>()
                .ForMember(
                  dest => dest.FirstName,
                  opt => opt.MapFrom(src => src.FirstName == null
                                  ? ""
                                  : src.FirstName))
                .ForMember(
                  dest => dest.LastName,
                  opt => opt.MapFrom(src => src.LastName == null
                                  ? ""
                                  : src.LastName))
            .ReverseMap();

            CreateMap<AuthorPostDto, Author>();
            CreateMap<AuthorPutDto, Author>();

        }

    }
}
