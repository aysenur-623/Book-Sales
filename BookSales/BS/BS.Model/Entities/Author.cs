using Infrastructure.Model;

namespace BS.Model.Entities
{
    public class Author : IEntity
    {
        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsActive { get; set; }


    }
}
