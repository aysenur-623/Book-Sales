using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class SaleRepository : BaseRepository<Sale, BookSalesContext>, ISaleRepository
    {
        public async Task<List<Sale>> GetByBookAsync(int bookId, params string[] includeList)
        {
            return await GetAllAsync(sle => sle.BookId == bookId);
        }
        public async Task<List<Sale>> GetByMemberAsync(int memberId, params string[] includeList)
        {
            return await GetAllAsync(sle => sle.MemberId == memberId);
        }
        public async Task<Sale> GetByIdAsync(int bookId, params string[] includeList)
        {
            return await GetAsync(sle => sle.BookId == bookId, includeList);
        }
    }
}
