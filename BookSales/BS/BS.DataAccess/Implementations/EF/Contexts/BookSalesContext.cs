using BS.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BS.DataAccess.Implementations.EF.Contexts
{
    public class BookSalesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=" +
                "DESKTOP-CAD0GE4;database=BookSales;trusted_connection=true;");
        }

        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<Sale>? Sales { get; set; }
        public DbSet<AdminPanelUser>? AdminPanelUsers { get; set; }
    }
}
