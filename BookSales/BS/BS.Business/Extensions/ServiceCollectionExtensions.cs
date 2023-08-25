using BS.Business.Implementations;
using BS.Business.Interfaces;
using BS.DataAccess.Implementations.EF.Repositories;
using BS.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BS.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(BookBs));

            services.AddScoped<IAuthorBs, AuthorBs>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<IBookBs, BookBs>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IGenreBs, GenreBs>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IMemberBs, MemberBs>();
            services.AddScoped<IMemberRepository, MemberRepository>();

            services.AddScoped<IPublisherBs, PublisherBs>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();

            services.AddScoped<ISaleBs, SaleBs>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddScoped<IAdminPanelUserBs, AdminPanelUserBs>();
            services.AddScoped<IAdminPanelUserRepository, AdminPanelUserRepository>();
        }
    }
}
