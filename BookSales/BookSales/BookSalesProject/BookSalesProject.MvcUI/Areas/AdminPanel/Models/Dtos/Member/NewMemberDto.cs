namespace BookSalesProject.MvcUI.Areas.AdminPanel.Models.Dtos.Member
{
    public class NewMemberDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
