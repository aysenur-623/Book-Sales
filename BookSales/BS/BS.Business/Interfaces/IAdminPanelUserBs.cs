using BS.Model.Dtos.AdminPanelUser;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface IAdminPanelUserBs
    {
        Task<ApiResponse<AdminPanelUserDto>> LogInAsync(string userName, string password, params string[] includeList);
    }
}
