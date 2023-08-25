using Infrastructure.Model;

namespace BS.Model.Dtos.Book
{
    public class BookPutDto : IDto
    {
        public int BookId { get; set; }
        public string? PicturePath { get; set; }
        public string? BookName { get; set; }
        public DateTime PublishDate { get; set; }
        public int NumberOfPages { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
