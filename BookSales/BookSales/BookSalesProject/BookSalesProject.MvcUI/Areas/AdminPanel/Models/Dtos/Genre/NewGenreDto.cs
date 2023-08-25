namespace BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Genre
{
    public class NewGenreDto
    {
        public int GenreId { get; set; }
        public string? GenreName { get; set; }
        public bool? IsActive { get; set; }
    }
}
