namespace BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Sale
{
    public class NewSaleDto
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int? NumberOfSales { get; set; }
        public DateTime? SaleTime { get; set; }
    }
}
