using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class BookRepository : BaseRepository<Book, BookSalesContext>, IBookRepository
    {
        public async Task<List<Book>> GetByGenreAsync(int genreId, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.GenreId == genreId, includeList);
        }

        public async Task<List<Book>> GetByNameAsync(string name, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.BookName == name, includeList);
        }
        public async Task<List<Book>> GetByNumberOfPagesAsync(int numberOfPages, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.NumberOfPages == numberOfPages, includeList);
        }
        public async Task<List<Book>> GetByISBNAsync(string isbn, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.ISBN == isbn, includeList);
        }
        public async Task<List<Book>> GetByPriceAsync(decimal min, decimal max, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.Price > min && pub.Price < max, includeList);
        }

        public async Task<Book> GetByIdAsync(int bookId, params string[] includeList)
        {
            return await GetAsync(pub => pub.BookId == bookId, includeList);
        }

        public async Task<bool> IsBookExistsWithName(string bookName)
        {
            var books = await GetAllAsync(x => x.BookName == bookName);

            if (books != null && books.Count > 0)
                return true; // bu isimli bir kategori var

            return false; // bu isimli bir kategori yok
        }
    }
}
