using _01_Framework.Infrastructure;
using MB.Domain.ArticleCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace MB.Infrastructure.EFCore.Repositories
{
    public class ArticleCategoryRepository :BaseRepository<long,ArticleCategory>, IArticleCategoryRepository
    {

        private readonly MasterBloggerContext _context;
        public ArticleCategoryRepository(MasterBloggerContext context) : base(context)
        {
            _context = context;
        }
        
  
        public List<ArticleCategory> GetList()
        {
            return _context.ArticleCategories.OrderByDescending(x=>x.Id).ToList();
        }

    
    }
}
 