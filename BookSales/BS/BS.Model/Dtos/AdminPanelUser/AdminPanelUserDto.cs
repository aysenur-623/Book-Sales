using Infrastructure.Model;

namespace BS.Model.Dtos.AdminPanelUser
{
    public class AdminPanelUserDto : IDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
