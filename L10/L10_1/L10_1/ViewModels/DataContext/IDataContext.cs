using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_1.ViewModels.DataContext
{
    public interface IDataContext
    {
        List<ArticleViewModel> GetArticles();
        //List<ArticleViewModel> GetArticles(CategoryViewModel category);
        
        ArticleViewModel GetArticle(int id);
        void AddArticle(ArticleViewModel article);
        void UpdateArticle(ArticleViewModel article);
        public void RemoveArticle(int id, string uploadFile);

        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategory(string name);
        void AddCategory(CategoryViewModel category);
        void UpdateCategory(string oldName, CategoryViewModel category);
        void RemoveCategory(CategoryViewModel category);

        string GetPhotosDestinationFile();

        bool DoesCategoryExist(string name);
        bool IsCategoryUsed(int inx);
    }
}
