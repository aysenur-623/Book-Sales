using AutoMapper;
using BS.Model.Dtos.Book;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookGetDto>()
                .ForMember(
                   dest => dest.GenreName,
                   opt => opt.MapFrom(src => src.Genre == null
                                   ? ""
                                   : src.Genre.GenreName))
                .ForMember(
                  dest => dest.BookName,
                  opt => opt.MapFrom(src => src.BookName == null
                                  ? ""
                                  : src.BookName))
               .ReverseMap();

            CreateMap<BookPostDto, Book>();
            CreateMap<BookPutDto, Book>();

        }

    }
}
