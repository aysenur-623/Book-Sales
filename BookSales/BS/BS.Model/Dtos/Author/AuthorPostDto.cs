using Infrastructure.Model;

namespace BS.Model.Dtos.Author
{
    public class AuthorPostDto : IDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }
    }
}
