using BS.Model.Dtos.Sale;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface ISaleBs
    {
        Task<ApiResponse<List<SaleGetDto>>> GetSalesAsync(params string[] incluedList);
        Task<ApiResponse<List<SaleGetDto>>> GetSalesByBookAsync(int bookId, params string[] incluedList);
        Task<ApiResponse<List<SaleGetDto>>> GetSalesByMemberAsync(int memberId, params string[] incluedList);

        Task<ApiResponse<SaleGetDto>> GetByIdAsync(int saleId, params string[] incluedList);
        Task<ApiResponse<Sale>> InsertAsync(SalePostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(SalePutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
