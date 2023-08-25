using Infrastructure.Model;

namespace BS.Model.Dtos.Sale
{
    public class SalePutDto : IDto
    {
        public int SaleId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int? NumberOfSales { get; set; }
        public DateTime? SaleTime { get; set; }
        public bool? IsActive { get; set; }
    }
}
