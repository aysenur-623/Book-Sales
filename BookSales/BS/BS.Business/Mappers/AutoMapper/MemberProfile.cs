using AutoMapper;
using BS.Model.Dtos.Member;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberGetDto>()
                 .ForMember(
                  dest => dest.FirstName,
                  opt => opt.MapFrom(src => src.FirstName == null
                                  ? ""
                                  : src.FirstName))

                 .ForMember(
                  dest => dest.Email,
                  opt => opt.MapFrom(src => src.Email == null

                                  ? ""
                                  : src.Email))
                .ForMember(
                  dest => dest.Address,
                  opt => opt.MapFrom(src => src.Address == null
                                  ? ""
                                  : src.Address.ToUpper()))
                .ReverseMap();

            CreateMap<MemberPostDto, Member>();
            CreateMap<MemberPutDto, Member>();

        }

    }
}
