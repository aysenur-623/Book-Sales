using BS.Model.Entities;
using Infrastructure.DataAccess;

namespace BS.DataAccess.Interfaces
{
    public interface IPublisherRepository : IBaseRepository<Publisher>
    {
        Task<List<Publisher>> GetByNameAsync(string name, params string[] includeList);
        Task<List<Publisher>> GetByCityAsync(string city, params string[] includeList);

        Task<Publisher> GetByIdAsync(int publisherId, params string[] includeList);
    }
}
