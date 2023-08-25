using BS.Model.Entities;
using Infrastructure.DataAccess;

namespace BS.DataAccess.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<List<Author>> GetByNameAsync(string name, params string[] includeList);

        Task<Author> GetByIdAsync(int authorId, params string[] includeList);
    }
}
