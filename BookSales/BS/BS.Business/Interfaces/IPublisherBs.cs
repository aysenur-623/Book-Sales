using BS.Model.Dtos.Publisher;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;

namespace BS.Business.Interfaces
{
    public interface IPublisherBs
    {
        Task<ApiResponse<List<PublisherGetDto>>> GetPublishersAsync(params string[] incluedList);
        Task<ApiResponse<List<PublisherGetDto>>> GetPublishersByNameAsync(string name, params string[] incluedList);
        Task<ApiResponse<List<PublisherGetDto>>> GetPublishersByCityAsync(string city, params string[] incluedList);

        Task<ApiResponse<PublisherGetDto>> GetByIdAsync(int publisherId, params string[] incluedList);
        Task<ApiResponse<Publisher>> InsertAsync(PublisherPostDto dto);
        Task<ApiResponse<NoData>> UpdateAsync(PublisherPutDto dto);
        Task<ApiResponse<NoData>> DeleteAsync(int id);
    }
}
