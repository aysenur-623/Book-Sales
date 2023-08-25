using BS.Model.Dtos.Author;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface IAuthorBs
    {
        Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsAsync(params string[] incluedList);
        Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsByNameAsync(string name, params string[] incluedList);

        Task<ApiResponse<AuthorGetDto>> GetByIdAsync(int authorId, params string[] incluedList);
        Task<ApiResponse<Author>> InsertAsync(AuthorPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(AuthorPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
