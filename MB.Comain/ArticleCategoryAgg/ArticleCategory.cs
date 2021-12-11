using MB.Domain.ArticleAgg;
using MB.Domain.ArticleCategoryAgg.Services;
using System;
using System.Collections.Generic;

namespace MB.Domain.ArticleCategoryAgg
{
    public class ArticleCategory
    {
        public long Id { get; set; }
        public string Title { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreationDate { get; private set; }


        public ICollection<Article> Articles { get; set; }
        public void GuardAgainstEmptyTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();
        }


        public ArticleCategory(string title, IArticleCategoryValidatorService validatorService)
        {
            GuardAgainstEmptyTitle(title);
            validatorService.CheckThatThisRecordAlreadyExists(title);
            Title = title;
            IsDeleted = false;
            CreationDate = DateTime.Now;
            Articles = new List<Article>();
        }

        public void Rename(string title)
        {
            GuardAgainstEmptyTitle(title);
            Title = title;

        }

        public void Remove()
        {
            IsDeleted = true;
        }
        public void Activate()
        {
            IsDeleted = false;
        }
    }
}
