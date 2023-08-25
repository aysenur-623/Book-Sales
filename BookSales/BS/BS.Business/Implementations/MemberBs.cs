using AutoMapper;
using BS.Business.CustomExceptions;
using BS.Business.Interfaces;
using BS.DataAccess.Interfaces;
using BS.Model.Dtos.Member;
using BS.Model.Entities;
using Infrastructure.Utilities.ApiResponses;
using Microsoft.AspNetCore.Http;

namespace BS.Business.Implementations
{
    public class MemberBs : IMemberBs
    {
        private readonly IMemberRepository _repo;
        private readonly IMapper _mapper;
        public MemberBs(IMemberRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<ApiResponse<MemberGetDto>> GetByIdAsync(int memberId, params string[] incluedList)
        {
            if (memberId <= 0)
                throw new BadRequestException("id değeri 0 dan büyük bir değer olmalıdır");

            var member = await _repo.GetByIdAsync(memberId, incluedList);

            if (member == null)
                throw new NotFoundException("bu id li kategori bulunamadı");

            var dto = _mapper.Map<MemberGetDto>(member);

            return ApiResponse<MemberGetDto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<ApiResponse<List<MemberGetDto>>> GetMembersAsync(params string[] incluedList)
        {
            var members = await _repo.GetAllAsync(m => m.IsActive == true, includeList: incluedList);

            if (members.Count > 0)
            {
                var returnList = _mapper.Map<List<MemberGetDto>>(members);

                return ApiResponse<List<MemberGetDto>>.Success(StatusCodes.Status200OK, returnList);
            }

            throw new NotFoundException("kaynak bulunamadı");

        }

        public async Task<ApiResponse<List<MemberGetDto>>> GetMembersByNameAsync(string name, params string[] incluedList)
        {

            var members = await _repo.GetByNameAsync(name, incluedList);

            if (name.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<MemberGetDto>>(members);

            return ApiResponse<List<MemberGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }
        public async Task<ApiResponse<List<MemberGetDto>>> GetMembersByEmailAsync(string email, params string[] incluedList)
        {
            var members = await _repo.GetByEmailAsync(email, incluedList);

            if (email.Length < 3)
                throw new BadRequestException("email en az 3 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<MemberGetDto>>(members);

            return ApiResponse<List<MemberGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }

        public async Task<ApiResponse<List<MemberGetDto>>> GetMembersByAddressAsync(string address, params string[] incluedList)
        {
            var members = await _repo.GetByAddressAsync(address, incluedList);

            if (address.Length < 3)
                throw new BadRequestException("adres en az 3 karakterden oluşmalıdır");

            var returnList = _mapper.Map<List<MemberGetDto>>(members);

            return ApiResponse<List<MemberGetDto>>.Success(StatusCodes.Status200OK, returnList);

        }
        public async Task<ApiResponse<Member>> InsertAsync(MemberPostDto dto)
        {

            if (dto.FirstName.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            if (dto.Email.Length < 2)
                throw new BadRequestException("email en az 2 karakterden oluşmalıdır");

            if (dto.Address.Length < 2)
                throw new BadRequestException("adres en az 2 karakterden oluşmalıdır");

            var entity = _mapper.Map<Member>(dto);
            entity.IsActive = true;
            var insertedMember = await _repo.InsertAsync(entity);

            return ApiResponse<Member>.Success(StatusCodes.Status201Created, insertedMember);
        }

        public async Task<ApiResponse<NoData>> UpdateAsync(MemberPutDto dto)
        {
            if (dto.MemberId <= 0)
                throw new BadRequestException("id pozitif bir değer olmalıdır");

            if (dto.FirstName.Length < 2)
                throw new BadRequestException("isim en az 2 karakterden oluşmalıdır");

            if (dto.Email.Length < 3)
                throw new BadRequestException("email en az 3 karakterden oluşmalıdır");

            if (dto.Address.Length < 2)
                throw new BadRequestException("adres en az 2 karakterden oluşmalıdır");

            var entity = _mapper.Map<Member>(dto);
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
