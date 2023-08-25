using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class GenreRepository : BaseRepository<Genre, BookSalesContext>, IGenreRepository
    {
        public async Task<List<Genre>> GetByNameAsync(string name, params string[] includeList)
        {
            return await GetAllAsync(gen => gen.GenreName == name);
        }

        public async Task<Genre> GetByIdAsync(int genreId, params string[] includeList)
        {
            return await GetAsync(gen => gen.GenreId == genreId, includeList);
        }
    }
}