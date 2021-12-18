using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_1.ViewModels.DataContext
{
    public class MockDataContext : IDataContext
    {
        List<CategoryViewModel> categories = new List<CategoryViewModel>
        {
            new CategoryViewModel("cat1"),
            new CategoryViewModel("cat2")
        };
        List<ArticleViewModel> articles = new List<ArticleViewModel>
        {
            new ArticleViewModel(1, "art1", 1.5),
            new ArticleViewModel(2, "art2", 3.3)
        };

        public void AddArticle(ArticleViewModel article)
        {
            int nextNumber = articles.Max(s => s.Id) + 1;
            article.Id = nextNumber;
            articles.Add(article);
        }

        public void AddCategory(CategoryViewModel category)
        {
            categories.Add(category);
        }

        public List<ArticleViewModel> GetArticles()
        {
            return articles;
        }

        public List<CategoryViewModel> GetCategories()
        {
            return categories;
        }

        public void UpdateCategory(CategoryViewModel category)
        {
            CategoryViewModel studToUpdate = categories.FirstOrDefault(s => s.Name == category.Name);
            categories = categories.Select(s => (s.Name == category.Name) ? category : s).ToList();
        }
    }
}
