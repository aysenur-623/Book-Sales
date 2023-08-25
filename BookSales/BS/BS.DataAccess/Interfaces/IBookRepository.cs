using BS.Model.Entities;
using Infrastructure.DataAccess;

namespace BS.DataAccess.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<List<Book>> GetByNumberOfPagesAsync(int numberOfPages, params string[] includeList);
        Task<List<Book>> GetByNameAsync(string name, params string[] includeList);
        Task<List<Book>> GetByISBNAsync(string isbn, params string[] includeList);
        Task<List<Book>> GetByPriceAsync(decimal min, decimal max, params string[] includeList);
        Task<List<Book>> GetByGenreAsync(int genreId, params string[] includeList);


        Task<Book> GetByIdAsync(int bookId, params string[] includeList);

        // Bu isimle bir kategori var mı ?
        Task<bool> IsBookExistsWithName (string? bookName);
    }
}



