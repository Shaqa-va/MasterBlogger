using Microsoft.Extensions.DependencyInjection;
using MB.Application;
using MB.Application.Contracts.ArticleCategory;
using MB.Domain.ArticleCategoryAgg;
using MB.Infrastructure.EFCore;
using MB.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using MB.Application.Contracts.Article;
using MB.Domain.ArticleAgg;
using MB.Domain.ArticleCategoryAgg.Services;
using MB.Infrastructure.Query;
using MB.Application.Contracts.Comment;
using MB.Domain.CommentAgg;

namespace MB.Infrastructure.Core
{
    public class Bootstrapper   
    {
        public static void Config(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleCategoryValidatorService, ArticleCategoryValidatorService>();

            services.AddTransient<IArticleApplication, ArticleApplication>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleCategoryValidatorService, ArticleCategoryValidatorService>();


            services.AddTransient<ICommentApplication, CommentApplication>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<IArticleQuery, ArticleQuery>();
            services.AddDbContext<MasterBloggerContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
