
using _01_Framework.Infrastructure;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IArticleCategoryValidatorService _articleCategoryValidatorService;

        public ArticleCategoryApplication(IUnitOfWork unitOfWork,IArticleCategoryValidatorService articleCategoryValidatorService, IArticleCategoryRepository articleCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _articleCategoryRepository = articleCategoryRepository;
            _articleCategoryValidatorService = articleCategoryValidatorService;
        }

        public void Create(CreateArticleCategory command)
        {
            _unitOfWork.BeginTran();
            var articleCategory = new ArticleCategory(command.Title,_articleCategoryValidatorService);
            _articleCategoryRepository.Create(articleCategory);
            _unitOfWork.CommitTran();
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
            _unitOfWork.BeginTran();
            var articleCategory = _articleCategoryRepository.Get(id);
            articleCategory.Activate();
            _unitOfWork.CommitTran();
            //_articleCategoryRepository.Save();
        }

        public void Remove(long id)
        {
            _unitOfWork.BeginTran();
            var articleCategory = _articleCategoryRepository.Get(id);
            articleCategory.Remove();
            _unitOfWork.CommitTran();
            //_articleCategoryRepository.Save();
        }

        public void Rename(RenameArticleCategory command)
        {
            _unitOfWork.BeginTran();
            var articleCatogory = _articleCategoryRepository.Get(command.Id);
            articleCatogory.Rename(command.Title);
            _unitOfWork.CommitTran();
            //_articleCategoryRepository.Save();
        }
    }
}
