using Infrastructure.Model;

namespace BS.Model.Dtos.Genre
{
    public class GenrePostDto : IDto
    {
        public string? GenreName { get; set; }
        public bool? IsActive { get; set; }
    }
}
