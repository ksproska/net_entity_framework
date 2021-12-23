using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using L10_1.ViewModels;

namespace L10_1.ViewModels.DataContext
{
    public class MockDataContext : IDataContext
    {
        public ShopViewModel shopViewModel = new ShopViewModel();
        List<ArticleViewModel> articles;
        public string uploadFolder;
        //public static string UPLOAD = "upload";
        //public static string IMAGE = "image";

        public MockDataContext()
        {
            ArticleViewModel.AllCategories = new List<CategoryViewModel>
            {
                new CategoryViewModel("tools"),
                new CategoryViewModel("toys"),
                new CategoryViewModel("painting"),
                new CategoryViewModel("food")
            };
            //string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "upload");
            articles = new List<ArticleViewModel>() { };

            this.AddArticle(new ArticleViewModel("car", 1.5, 1, null));
            this.AddArticle(new ArticleViewModel("screwdriver", 1.5, 0, null));
            this.AddArticle(new ArticleViewModel("doll", 3.3, 1, null));
            this.AddArticle(new ArticleViewModel("apple", 3.3, 3, null));
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

        public void RemoveArticle(int id, string uploadFile)
        {
            var article = GetArticle(id);
            articles.Remove(article);
            if(article.FormFile != null)
            {
                string path = Path.GetFullPath(Path.Combine(uploadFile, article.FormFile.FileName));
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public void RemoveCategory(string name)
        {
            var cat = ArticleViewModel.AllCategories.FirstOrDefault(s => s.Name.Equals(name));
            int inx = ArticleViewModel.AllCategories.IndexOf(cat);
            if (GetArticles(inx).Count == 0 && inx != -1)
            {
                foreach(var a in articles)
                {
                    if(a.CategoryInx > inx)
                    {
                        a.CategoryInx -= 1;
                    }
                }
                ArticleViewModel.AllCategories.RemoveAt(inx);
            }
            
        }

        public void UpdateArticle(ArticleViewModel article)
        {
            ArticleViewModel articleToUpdate = articles.FirstOrDefault(s => s.Id == article.Id);
            article.FormFile = articleToUpdate.FormFile;
            articles = articles.Select(s => (s.Id == article.Id) ? article : s).ToList();
        }

        public void UpdateCategory(string oldName, CategoryViewModel category)
        {
            CategoryViewModel cat = GetCategory(oldName);
            cat.Name = category.Name;
        }

        public string GetPhotosDestinationFile()
        {
            return ArticleViewModel.UPLOAD;
        }

        public bool DoesCategoryExist(string name)
        {
            var matches = ArticleViewModel.AllCategories.Where(p => p.Name == name).ToList();
            return matches.Count > 0;
        }

        public bool IsCategoryUsed(int inx)
        {
            var matches = articles.Where(p => p.CategoryInx == inx).ToList();
            return matches.Count > 0;
        }

        public CategoryViewModel GetCategory(string name)
        {
            CategoryViewModel cat = ArticleViewModel.AllCategories.FirstOrDefault(s => s.Name == name);
            return cat;
        }

        public List<ArticleViewModel> GetArticles(int id)
        {
            var matches = articles.Where(p => p.CategoryInx == id).ToList();
            return matches;
        }

        public ShopViewModel GetShopViewModel()
        {
            return shopViewModel;
        }

        public bool IsCategoryUsed(CategoryViewModel category)
        {
            int inx = ArticleViewModel.AllCategories.IndexOf(category);
            return GetArticles(inx).Count != 0;
        }
    }
}
