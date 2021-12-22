
using MB.Application.Contracts.ArticleCategory;
using MB.Domain.ArticleCategoryAgg;
using MB.Domain.ArticleCategoryAgg.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MB.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleCategoryValidatorService _articleCategoryValidatorService;

        public ArticleCategoryApplication(IArticleCategoryValidatorService articleCategoryValidatorService, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _articleCategoryValidatorService = articleCategoryValidatorService;
        }

        public void Create(CreateArticleCategory command)
        {
            var articleCategory = new ArticleCategory(command.Title,_articleCategoryValidatorService);
            _articleCategoryRepository.Create(articleCategory);
        }

        public RenameArticleCategory Get(long id)
        {
            var articleCategory = _articleCategoryRepository.Get(id);
            return new RenameArticleCategory
            {
                Id = articleCategory.Id,
             Title = articleCategory.Title
            };
        }

        public List<ArticleCategoryViewModel> List()
        {
            var articleCategories = _articleCategoryRepository.GetAll();
            var result = new List<ArticleCategoryViewModel>();
            foreach(var articleCategory in articleCategories)
            {
               result.Add(new ArticleCategoryViewModel
               {
                   Id = articleCategory.Id,
                   Title = articleCategory.Title,
                   IsDeleted = articleCategory.IsDeleted,
                   CreationDate = articleCategory.CreationDate.ToString(CultureInfo.InvariantCulture)
               });

            }

            return result.OrderByDescending(x=>x.Id).ToList();
        }

        public void Activate(long id)
        {
            var articleCategory = _articleCategoryRepository.Get(id);
            articleCategory.Activate();
            //_articleCategoryRepository.Save();
        }

        public void Remove(long id)
        {
            var articleCategory = _articleCategoryRepository.Get(id);
            articleCategory.Remove();
            //_articleCategoryRepository.Save();
        }

        public void Rename(RenameArticleCategory command)
        {
            var articleCatogory = _articleCategoryRepository.Get(command.Id);
            articleCatogory.Rename(command.Title);
            //_articleCategoryRepository.Save();
        }
    }
}
