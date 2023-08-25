using AutoMapper;
using BS.Business.CustomExceptions;
using BS.Business.Interfaces;
using BS.DataAccess.Interfaces;
using BS.Model.Dtos.Book;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace BS.Business.Implementations
{
    public class BookBs : IBookBs
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;
        public BookBs(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<ApiResponse<BookGetDto>>GetByIdAsync(int bookId, params string[] incluedList)
        {
            if (bookId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var book = await _repo.GetByIdAsync(bookId, incluedList);

            if (book == null)
                throw new NotFoundException("bu id li kitap bulunamadı");

            var dto = _mapper.Map<BookGetDto>(book);

            return ApiResponse<BookGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByGenreAsync(int genreId, params string[] includeList)
        {
            if (genreId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var books = await _repo.GetByGenreAsync(genreId, includeList);

            if (books.Count > 0)
            {
                var returnList = _mapper.Map<List<BookGetDto>>(books);

                return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");
        }

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksAsync(params string[] incluedList)
        {
            var books = await _repo.GetAllAsync(b => b.IsActive == true, includeList: incluedList);

            if (books.Count > 0)
            {
                var returnList = _mapper.Map<List<BookGetDto>>(books);

                return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");

        }

        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByISBNAsync(string isbn, params string[] incluedList)
        {

            var books = await _repo.GetByISBNAsync(isbn, incluedList);

            if (isbn.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }


        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByNumberOfPagesAsync(int numberOfPages, params string[] incluedList)
        {
            if (numberOfPages <= 0)
                throw new BadRequestException("sayfa sayısı 0 dan büyük bir değer olmalıdır.");

            var numberOfPageses = await _repo.GetByNumberOfPagesAsync(numberOfPages, incluedList);

            if (numberOfPageses.Count > 0)
            {
                var returnList = _mapper.Map<List<BookGetDto>>(numberOfPageses);

                return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new BadRequestException("kaynak bulunamadı.");


        }
        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByNameAsync(string name, params string[] incluedList)
        {

            var books = await _repo.GetByNameAsync(name, incluedList);

            if (name.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<BookGetDto>>(books);

            return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }
        public async Task<ApiResponse<List<BookGetDto>>> GetBooksByPriceAsync(decimal min, decimal max, params string[] incluedList)
        {


            if (min > max)
                throw new BadRequestException("min değer max değerden büyük olamaz");

            if (min < 0 || max < 0)
                throw new BadRequestException("fiyatlar pozitif değer olmalıdır");


            var products = await _repo.GetByPriceAsync(min, max, incluedList);

            if (products.Count > 0)
            {
                var returnList = _mapper.Map<List<BookGetDto>>(products);

                return ApiResponse<List<BookGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");


        }

        public async Task<ApiResponse<Book>> InsertAsync(BookPostDto dto)
        {
            // var olan kategori ismi ile aynı isim gönderilemesin 

            if (await _repo.IsBookExistsWithName(dto.BookName))
                throw new BadRequestException("Böyle bir kitap bulunuyor.");

            if (dto?.BookName?.Length < 2)
                throw new BadRequestException("Kitap adı en az 2 karakterden oluşmalıdır");

            if (dto?.NumberOfPages <= 0)
                throw new BadRequestException("Sayfa sayısı pozitif bir değer olmalıdır");

            if (dto?.ISBN?.Length < 2)
                throw new BadRequestException("ISBN en az 2 karakterden oluşmalıdır");

            if (dto?.Price <= 0)
                throw new BadRequestException("Fiyat pozitif bir değer olmalıdır");


        

            var entity = _mapper.Map<Book>(dto);
            entity.IsActive = true;
            var insertedBook = await _repo.InsertAsync(entity);
            return ApiResponse<Book>.Success(StatusCodes.Status201Created, insertedBook);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(BookPutDto dto)
        {
            if (dto.BookId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            if (dto.BookName.Length < 2)
                throw new BadRequestException("Kitap adı en az 2 karakterden oluşmalıdır");

            if (dto.NumberOfPages <= 0)
                throw new BadRequestException("Sayfa sayısı pozitif bir değer olmalıdır");

            if (dto.ISBN.Length < 2)
                throw new BadRequestException("ISBN en az 2 karakterden oluşmalıdır");

            if (dto.Price <= 0)
                throw new BadRequestException("Fiyat pozitif bir değer olmalıdır");


            
            var entity = _mapper.Map<Book>(dto);
            entity.IsActive = true;
            await _repo.UpdateAsync(entity);
            
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            

           

            

        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            var entity = await _repo.GetByIdAsync(id);
            entity.IsActive = false;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        
    }
}


