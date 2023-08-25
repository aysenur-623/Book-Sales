using AutoMapper;
using BS.Business.CustomExceptions;
using BS.Business.Interfaces;
using BS.DataAccess.Interfaces;
using BS.Model.Dtos.Genre;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace BS.Business.Implementations
{
    public class GenreBs :IGenreBs
    {
        private readonly IGenreRepository _repo;
        private readonly IMapper _mapper;
        public GenreBs(IGenreRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<ApiResponse<GenreGetDto>> GetByIdAsync(int genreId, params string[] incluedList)
        {
            if (genreId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var genre = await _repo.GetByIdAsync(genreId, incluedList);

            if (genre == null)
                throw new NotFoundException("bu id li kategori bulunamadı");

            var dto = _mapper.Map<GenreGetDto>(genre);

            return ApiResponse<GenreGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<GenreGetDto>>> GetGenresAsync(params string[] incluedList)
        {
            var genres = await _repo.GetAllAsync(a => a.IsActive == true, includeList: incluedList);

            if (genres.Count > 0)
            {
                var returnList = _mapper.Map<List<GenreGetDto>>(genres);

                return ApiResponse<List<GenreGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");

        }

        public async Task<ApiResponse<List<GenreGetDto>>> GetGenresByNameAsync(string name, params string[] incluedList)
        {

            var genres = await _repo.GetByNameAsync(name, incluedList);

            if (name.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<GenreGetDto>>(genres);

            return ApiResponse<List<GenreGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }

        public async Task<ApiResponse<Genre>> InsertAsync(GenrePostDto dto)
        {
            if (dto.GenreName.Length < 2)
                throw new BadRequestException("tür adı en az 2 karakterden oluşmalıdır");

            var entity = _mapper.Map<Genre>(dto);
            entity.IsActive = true;
            var insertedGenre = await _repo.InsertAsync(entity);

            return ApiResponse<Genre>.Success(StatusCodes.Status201Created, insertedGenre);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(GenrePutDto dto)
        {
            if (dto.GenreId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            if (dto.GenreName.Length < 2)
                throw new BadRequestException("tür adı en az 2 karakterden oluşmalıdır");


            var entity = _mapper.Map<Genre>(dto);
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

