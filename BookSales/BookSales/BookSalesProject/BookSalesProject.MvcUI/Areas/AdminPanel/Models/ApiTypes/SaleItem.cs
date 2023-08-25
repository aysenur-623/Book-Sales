namespace BookSalesProject.MvcUI.Areas.AdminPanel.Models.ApiTypes
{
    public class SaleItem
    {
        public int SaleId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int? NumberOfSales { get; set; }
        public DateTime? SaleTime { get; set; }
    }
}
