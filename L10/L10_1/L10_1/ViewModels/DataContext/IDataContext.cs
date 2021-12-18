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
        List<CategoryViewModel> GetCategories();
        void AddArticle(ArticleViewModel article);
        ////void RemoveArticle(int id);
        //void UpdateArticle(ArticleViewModel article);
        void AddCategory(CategoryViewModel category);
        void UpdateCategory(CategoryViewModel category);
        //void RemoveCategory(string name);
    }
}
