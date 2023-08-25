using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class AdminPanelUserRepository : BaseRepository<AdminPanelUser, BookSalesContext>, IAdminPanelUserRepository
    {
        public async Task<AdminPanelUser> GetByUserNameAndPasswordAsync(string userName, string password, params string[] includeList)
        {
            return await GetAsync(x => x.UserName == userName && x.Password == password && x.IsActive.Value, includeList);

        }
    }
}
