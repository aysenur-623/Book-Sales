using AutoMapper;
using BS.Model.Dtos.Publisher;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherGetDto>()
                .ForMember(
                  dest => dest.PublisherName,
                  opt => opt.MapFrom(src => src.PublisherName == null
                                  ? ""
                                  : src.PublisherName))
                .ForMember(
                  dest => dest.City,
                  opt => opt.MapFrom(src => src.City == null
                                  ? ""
                                  : src.City))
            .ReverseMap();

            CreateMap<PublisherPostDto, Publisher>();
            CreateMap<PublisherPutDto, Publisher>();

        }

    }
}
