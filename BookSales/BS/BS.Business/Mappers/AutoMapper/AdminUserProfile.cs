using AutoMapper;
using BS.Model.Dtos.AdminPanelUser;
using BS.Model.Entities;

namespace BS.Business.Mappers.AutoMapper
{
    public class AdminUserProfile : Profile
    {
        public AdminUserProfile()
        {
            CreateMap<AdminPanelUser, AdminPanelUserDto>();
        }
    }
}
