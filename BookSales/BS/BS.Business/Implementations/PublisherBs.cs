using AutoMapper;
using BS.Business.CustomExceptions;
using BS.Business.Interfaces;
using BS.DataAccess.Interfaces;
using BS.Model.Dtos.Publisher;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace BS.Business.Implementations
{
    public class PublisherBs : IPublisherBs
    {
        private readonly IPublisherRepository _repo;
        private readonly IMapper _mapper;
        public PublisherBs(IPublisherRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<ApiResponse<PublisherGetDto>> GetByIdAsync(int publisherId, params string[] incluedList)
        {
            if (publisherId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var publisher = await _repo.GetByIdAsync(publisherId, incluedList);

            if (publisher == null)
                throw new NotFoundException("bu id li kategori bulunamadı");

            var dto = _mapper.Map<PublisherGetDto>(publisher);

            return ApiResponse<PublisherGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<PublisherGetDto>>> GetPublishersAsync(params string[] incluedList)
        {
            var publishers = await _repo.GetAllAsync(p => p.IsActive == true,  includeList: incluedList);

            if (publishers.Count > 0)
            {
                var returnList = _mapper.Map<List<PublisherGetDto>>(publishers);

                return ApiResponse<List<PublisherGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");

        }

        public async Task<ApiResponse<List<PublisherGetDto>>> GetPublishersByNameAsync(string name, params string[] incluedList)
        {

            var publishers = await _repo.GetByNameAsync(name, incluedList);

            if (name.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<PublisherGetDto>>(publishers);

            return ApiResponse<List<PublisherGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }
        public async Task<ApiResponse<List<PublisherGetDto>>> GetPublishersByCityAsync(string city, params string[] incluedList)
        {
            var publishers = await _repo.GetByCityAsync(city, incluedList);

            if (city.Length < 3)
                throw new BadRequestException("şehir en az 3 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<PublisherGetDto>>(publishers);

            return ApiResponse<List<PublisherGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }

        public async Task<ApiResponse<Publisher>> InsertAsync(PublisherPostDto dto)
        {

            if (dto.PublisherName.Length < 2)
                throw new BadRequestException("yayın adı en az 2 karakterden oluşmalıdır");

            if (dto.City.Length < 2)
                throw new BadRequestException("şehir en az 2 karakterden oluşmalıdır");

            var entity = _mapper.Map<Publisher>(dto);
            entity.IsActive = true;
            var insertedPublisher = await _repo.InsertAsync(entity);

            return ApiResponse<Publisher>.Success(StatusCodes.Status201Created, insertedPublisher);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(PublisherPutDto dto)
        {
            if (dto.PublisherId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            if (dto.PublisherName.Length < 2)
                throw new BadRequestException("Yayın adı en az 2 karakterden oluşmalıdır");

            if (dto.City.Length < 3)
                throw new BadRequestException("şehir en az 3 karakterden oluşmalıdır");

            var entity = _mapper.Map<Publisher>(dto);
            entity.IsActive = true;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<NoData>> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            var entity = await _repo.GetByIdAsync(id);
            entity.IsActive = false;
            await _repo.UpdateAsync(entity);

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }
    }
}
