using BS.DataAccess.Implementations.EF.Contexts;
using BS.DataAccess.Interfaces;
using BS.Model.Entities;
using Infrastructure.DataAccess.EF;

namespace BS.DataAccess.Implementations.EF.Repositories
{
    public class MemberRepository : BaseRepository<Member, BookSalesContext>, IMemberRepository
    {
        public async Task<List<Member>> GetByNameAsync(string name, params string[] includeList)
        {
            return await GetAllAsync(mbr => mbr.FirstName == name);
        }
        public async Task<List<Member>> GetByEmailAsync(string email, params string[] includeList)
        {
            return await GetAllAsync(mbr => mbr.Email == email);
        }
        public async Task<List<Member>> GetByAddressAsync(string address, params string[] includeList)
        {
            return await GetAllAsync(mbr => mbr.Address == address);
        }
        public async Task<Member> GetByIdAsync(int memberId, params string[] includeList)
        {
            return await GetAsync(mbr => mbr.MemberId == memberId, includeList);
        }
    }
}
