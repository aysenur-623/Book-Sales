using Infrastructure.Model;

namespace BS.Model.Dtos.Publisher
{
    public class PublisherGetDto : IDto
    {
        public int PublisherId { get; set; }
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public bool? IsActive { get; set; }
    }
}
