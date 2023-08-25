using Infrastructure.Model;

namespace BS.Model.Dtos.Genre
{
    public class GenrePutDto : IDto
    {
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public bool? IsActive { get; set; }
    }
}
