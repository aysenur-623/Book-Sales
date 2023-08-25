using BS.Model.Dtos.Book;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface IBookBs
    {
        Task<ApiResponse<List<BookGetDto>>> GetBooksAsync(params string[] incluedList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByNumberOfPagesAsync(int numberOfPages, params string[] incluedList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByISBNAsync(string ISBN, params string[] incluedList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByNameAsync(string name, params string[] incluedList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByPriceAsync(decimal min, decimal max, params string[] includeList);
        Task<ApiResponse<List<BookGetDto>>> GetBooksByGenreAsync(int genreId, params string[] includeList);

        Task<ApiResponse<BookGetDto>> GetByIdAsync(int bookId, params string[] incluedList);
        Task<ApiResponse<Book>> InsertAsync(BookPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(BookPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
