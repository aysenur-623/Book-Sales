using BS.Model.Entities;
using Infrastructure.DataAccess;

namespace BS.DataAccess.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<List<Genre>> GetByNameAsync(string name, params string[] includeList);

        Task<Genre> GetByIdAsync(int authorId, params string[] includeList);
    }
}
