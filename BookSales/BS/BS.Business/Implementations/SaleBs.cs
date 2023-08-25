using AutoMapper;
using BS.Business.CustomExceptions;
using BS.Business.Interfaces;
using BS.DataAccess.Interfaces;
using BS.Model.Dtos.Sale;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace BS.Business.Implementations
{
    public class SaleBs : ISaleBs
    {
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;
        public SaleBs(ISaleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<ApiResponse<SaleGetDto>> GetByIdAsync(int saleId, params string[] incluedList)
        {
            if (saleId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var sale = await _repo.GetByIdAsync(saleId, incluedList);

            if (sale == null)
                throw new NotFoundException("bu id li kategori bulunamadı");

            var dto = _mapper.Map<SaleGetDto>(sale);

            return ApiResponse<SaleGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<SaleGetDto>>> GetSalesAsync(params string[] incluedList)
        {
            var sales = await _repo.GetAllAsync(s => s.IsActive == true, includeList: incluedList);

            if (sales.Count > 0)
            {
                var returnList = _mapper.Map<List<SaleGetDto>>(sales);

                return ApiResponse<List<SaleGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");

        }

        public async Task<ApiResponse<List<SaleGetDto>>> GetSalesByBookAsync(int bookId, params string[] incluedList)
        {
            if (bookId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır.");

            var books = await _repo.GetByBookAsync(bookId, incluedList);
            if (books.Count > 0)
            {
                var returnList = _mapper.Map<List<SaleGetDto>>(books);

                return ApiResponse<List<SaleGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new BadRequestException("kaynak bulunamadı.");


        }

        public async Task<ApiResponse<List<SaleGetDto>>> GetSalesByMemberAsync(int memberId, params string[] incluedList)
        {
            if (memberId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır.");

            var members = await _repo.GetByMemberAsync(memberId, incluedList);

            if (members.Count > 0)
            {
                var returnList = _mapper.Map<List<SaleGetDto>>(members);

                return ApiResponse<List<SaleGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new BadRequestException("kaynak bulunamadı.");


        }

        public async Task<ApiResponse<Sale>> InsertAsync(SalePostDto dto)
        {

            if (dto.BookId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            if (dto.MemberId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");


            var entity = _mapper.Map<Sale>(dto);
            entity.IsActive = true;
            var insertedSale = await _repo.InsertAsync(entity);

            return ApiResponse<Sale>.Success(StatusCodes.Status201Created, insertedSale);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(SalePutDto dto)
        {
            if (dto.SaleId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            if (dto.BookId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            if (dto.MemberId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");


            var entity = _mapper.Map<Sale>(dto);
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
