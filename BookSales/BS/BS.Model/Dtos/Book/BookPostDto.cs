using Infrastructure.Model;

namespace BS.Model.Dtos.Book
{
    public class BookPostDto : IDto
    {
        public string? BookName { get; set; }
        public string? PicturePath { get; set; }
        public DateTime PublishDate { get; set; }
        public int NumberOfPages { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
