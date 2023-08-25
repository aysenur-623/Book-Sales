using Infrastructure.Model;

namespace BS.Model.Entities
{
    public class Genre : IEntity
    {
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public bool? IsActive { get; set; }


        public List<Book>? Books { get; set; }
    }
}
