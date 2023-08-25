using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class AuthorRepository : BaseRepository<Author, BookSalesContext>, IAuthorRepository
    {
        public async Task<List<Author>> GetByNameAsync(string name, params string[] includeList)
        {
            return await GetAllAsync(atr => atr.FirstName == name);
        }

        public async Task<Author> GetByIdAsync(int authorId, params string[] includeList)
        {
            return await GetAsync(ctg => ctg.AuthorId == authorId, includeList);
        }
    }
}
