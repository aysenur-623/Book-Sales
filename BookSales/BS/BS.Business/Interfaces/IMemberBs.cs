using BS.Model.Dtos.Member;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface IMemberBs
    {
        Task<ApiResponse<List<MemberGetDto>>> GetMembersAsync(params string[] incluedList);
        Task<ApiResponse<List<MemberGetDto>>> GetMembersByNameAsync(string name, params string[] incluedList);
        Task<ApiResponse<List<MemberGetDto>>> GetMembersByEmailAsync(string email, params string[] incluedList);
        Task<ApiResponse<List<MemberGetDto>>> GetMembersByAddressAsync(string address, params string[] incluedList);

        Task<ApiResponse<MemberGetDto>> GetByIdAsync(int memberId, params string[] incluedList);
        Task<ApiResponse<Member>> InsertAsync(MemberPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(MemberPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
