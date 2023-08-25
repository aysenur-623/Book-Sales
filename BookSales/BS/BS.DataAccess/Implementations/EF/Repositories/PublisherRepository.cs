using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher, BookSalesContext>, IPublisherRepository
    {
        public async Task<List<Publisher>> GetByNameAsync(string name, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.PublisherName == name);
        }
        public async Task<List<Publisher>> GetByCityAsync(string city, params string[] includeList)
        {
            return await GetAllAsync(pub => pub.City == city);
        }

        public async Task<Publisher> GetByIdAsync(int publisherId, params string[] includeList)
        {
            return await GetAsync(ctg => ctg.PublisherId == publisherId, includeList);
        }
    }
}
