using _01_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MB.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        List<ArticleCategory> GetList();
    }
}
