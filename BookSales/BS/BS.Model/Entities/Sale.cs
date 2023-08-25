using Infrastructure.Model;

namespace BS.Model.Entities
{
    public class Sale : IEntity
    {
        public int SaleId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int? NumberOfSales { get; set; }
        public DateTime? SaleTime { get; set; }
        public bool? IsActive { get; set; }


    }
}
