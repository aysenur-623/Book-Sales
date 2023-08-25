using AutoMapper;
using BS.Business.CustomExceptions;
using BS.Business.Interfaces;
using BS.DataAccess.Interfaces;
using BS.Model.Dtos.Author;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace BS.Business.Implementations
{
    public class AuthorBs : IAuthorBs
    {
        private readonly IAuthorRepository _repo;
        private readonly IMapper _mapper;
        public AuthorBs(IAuthorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<ApiResponse<AuthorGetDto>> GetByIdAsync(int authorId, params string[] incluedList)
        {
            if (authorId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var author = await _repo.GetByIdAsync(authorId, incluedList);

            if (author == null)
                throw new NotFoundException("bu id li kategori bulunamadı");

            var dto = _mapper.Map<AuthorGetDto>(author);

            return ApiResponse<AuthorGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsAsync(params string[] incluedList)
        {
            var authors = await _repo.GetAllAsync(a => a.IsActive == true, includeList: incluedList);

            if (authors.Count > 0)
            {
                var returnList = _mapper.Map<List<AuthorGetDto>>(authors);

                return ApiResponse<List<AuthorGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");

        }

        public async Task<ApiResponse<List<AuthorGetDto>>> GetAuthorsByNameAsync(string name, params string[] incluedList)
        {

            var authors = await _repo.GetByNameAsync(name, incluedList);

            if (name.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<AuthorGetDto>>(authors);

            return ApiResponse<List<AuthorGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }

        public async Task<ApiResponse<Author>> InsertAsync(AuthorPostDto dto)
        {
            if (dto.FirstName.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            if (dto.LastName.Length < 3)
                throw new BadRequestException("soyad en az 3 karakterden oluşmalıdır");

            var entity = _mapper.Map<Author>(dto);
            entity.IsActive = true;
            var insertedAuthor = await _repo.InsertAsync(entity);

            return ApiResponse<Author>.Success(StatusCodes.Status201Created, insertedAuthor);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(AuthorPutDto dto)
        {
            if (dto.AuthorId <= 0)
                throw new BadRequestException("Id pozitif bir değer olmalıdır");

            if (dto.FirstName.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            if (dto.LastName.Length < 3)
                throw new BadRequestException("soyad en az 3 karakterden oluşmalıdır");


            var entity = _mapper.Map<Author>(dto);
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



