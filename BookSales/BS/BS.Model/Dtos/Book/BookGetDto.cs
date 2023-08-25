using Infrastructure.Model;

namespace BS.Model.Dtos.Book
{
    public class BookGetDto : IDto
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int? GenreId { get; set; }
        public string? BookName { get; set; }
        public string? GenreName { get; set; }
        public string? PicturePath { get; set; }
        public DateTime PublishDate { get; set; }
        public int NumberOfPages { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
