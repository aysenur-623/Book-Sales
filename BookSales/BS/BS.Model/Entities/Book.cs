using Infrastructure.Model;

namespace BS.Model.Entities
{
    public class Book : IEntity
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public int GenreId { get; set; }
        public string? PicturePath { get; set; }
        public string? BookName { get; set; }
        public DateTime PublishDate { get; set; }
        public int NumberOfPages { get; set; }
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }



        public Genre? Genre { get; set; }

    }
}
