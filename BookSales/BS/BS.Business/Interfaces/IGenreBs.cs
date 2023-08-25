using BS.Model.Dtos.Genre;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface IGenreBs
    {
        Task<ApiResponse<List<GenreGetDto>>> GetGenresAsync(params string[] incluedList);
        Task<ApiResponse<List<GenreGetDto>>> GetGenresByNameAsync(string name, params string[] incluedList);
   
        Task<ApiResponse<GenreGetDto>> GetByIdAsync(int genreId, params string[] incluedList);
        Task<ApiResponse<Genre>> InsertAsync(GenrePostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(GenrePutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
