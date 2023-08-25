using BS.Model.Entities;
using Infrastructure.DataAccess;

namespace BS.DataAccess.Interfaces
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        Task<List<Sale>> GetByBookAsync(int bookId, params string[] includeList);
        Task<List<Sale>> GetByMemberAsync(int memberId, params string[] includeList);

        Task<Sale> GetByIdAsync(int saleId, params string[] includeList);
    }
}
