using Infrastructure.Model;

namespace BS.Model.Dtos.Publisher
{
    public class PublisherPostDto : IDto
    {
        public string? PublisherName { get; set; }
        public string? City { get; set; }
        public bool? IsActive { get; set; }
    }
}
