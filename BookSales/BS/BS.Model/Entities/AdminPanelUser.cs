using Infrastructure.Model;

namespace BS.Model.Entities
{
    public class AdminPanelUser : IEntity
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
    }

}
