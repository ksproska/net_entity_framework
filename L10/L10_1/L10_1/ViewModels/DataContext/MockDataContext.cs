using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace L10_1.ViewModels.DataContext
{
    public class MockDataContext : IDataContext
    {
        List<ArticleViewModel> articles;

        public MockDataContext()
        {
            ArticleViewModel.AllCategories = new List<CategoryViewModel>
            {
                new CategoryViewModel("cat1"),
                new CategoryViewModel("cat2")
            };
            articles = new List<ArticleViewModel>() { };
            this.AddArticle(new ArticleViewModel("art1", 1.5, 1, "/image/Z1.png"));
            this.AddArticle(new ArticleViewModel("art2", 1.5, 0, ""));
            this.AddArticle(new ArticleViewModel("art3", 3.3, 1, "/image/Z2.png"));
        }

        public void AddArticle(ArticleViewModel article)
        {
            int nextNumber = articles.Count > 0 ? articles.Max(s => s.Id) + 1 : 0;
            article.Id = nextNumber;
            articles.Add(article);
        }

        public void AddCategory(CategoryViewModel category)
        {
            ArticleViewModel.AllCategories.Add(category);
        }

        public ArticleViewModel GetArticle(int id)
        {
            return articles.FirstOrDefault(s => s.Id == id);
        }

        public List<ArticleViewModel> GetArticles()
        {
            return articles;
        }

        public List<CategoryViewModel> GetCategories()
        {
            return ArticleViewModel.AllCategories;
        }

        public void RemoveArticle(int id)
        {
            var article = GetArticle(id);
            articles.Remove(article);
            //if (File.Exists($"wwwroot{article.ImagePath}"))
            //{
            //    File.Delete($"wwwroot{article.ImagePath}");
            //}

        }

        public void RemoveCategory(CategoryViewModel category)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(ArticleViewModel article)
        {
            ArticleViewModel articleToUpdate = articles.FirstOrDefault(s => s.Id == article.Id);
            article.ImagePath = articleToUpdate.ImagePath;
            articles = articles.Select(s => (s.Id == article.Id) ? article : s).ToList();
        }

        public void UpdateCategory(CategoryViewModel category)
        {
            CategoryViewModel categoryToUpdate = ArticleViewModel.AllCategories.FirstOrDefault(s => s.Name == category.Name);
            ArticleViewModel.AllCategories = ArticleViewModel.AllCategories.Select(s => (s.Name == category.Name) ? category : s).ToList();
        }


    }
}
