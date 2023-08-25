using BS.Model.Entities;
using Infrastructure.DataAccess;

namespace BS.DataAccess.Interfaces
{
    public interface IMemberRepository : IBaseRepository<Member>
    {
        Task<List<Member>> GetByNameAsync(string name, params string[] includeList);
        Task<List<Member>> GetByEmailAsync(string email, params string[] includeList);
        Task<List<Member>> GetByAddressAsync(string address, params string[] includeList);

        Task<Member> GetByIdAsync(int memberId, params string[] includeList);
    }
}
