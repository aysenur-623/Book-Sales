using Infrastructure.Model;

namespace BS.Model.Entities
{
    public class Publisher : IEntity
    {
        public int PublisherId { get; set; }
        public string? PublisherName { get; set;}
        public string? City { get; set;}
        public bool? IsActive { get; set; }

    }

}
