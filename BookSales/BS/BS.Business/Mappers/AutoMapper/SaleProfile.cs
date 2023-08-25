using AutoMapper;
using BS.Model.Dtos.Sale;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleGetDto>()
                .ForMember(
                  dest => dest.BookId,
                  opt => opt.MapFrom(src => src.BookId == null ? "": src.BookId.ToString()))
                .ForMember(
                  dest => dest.MemberId,
                  opt => opt.MapFrom(src => src.MemberId == null
                                  ? ""
                                  : src.MemberId.ToString()))
                .ReverseMap();

            CreateMap<SalePostDto, Sale>();
            CreateMap<SalePutDto, Sale>();

        }

    }
}
