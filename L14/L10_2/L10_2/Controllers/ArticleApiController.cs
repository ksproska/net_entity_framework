using L10_2.Data;
using L10_2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_2.Controllers
{
    [EnableCors]
    [Route("api/article")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ArticleApiController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public ArticleApiController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Article> Get() => _context.Article;
        [HttpGet("{id}")]
        public Article Get(int id) => _context.Article.ToList().Where(item => item.Id == id).FirstOrDefault();
        [HttpPost]
        public Article Post([FromBody] Article article)
        {
            _context.Article.Add(article);
            _context.SaveChanges();
            return article;
        }

        [HttpPut]
        public Article Put([FromBody] Article article)
        {
            _context.Article.Update(article);
            _context.SaveChanges();
            return article;
        }

        //[HttpPatch("{id}")]
        //public StatusCodeResult Patch(int id,
        //        [FromBody] JsonPatchDocument<Article> patch)
        //{
        //    //not working
        //    Article article = Get(id);
        //    if (article != null)
        //    {
        //        patch.ApplyTo(article);
        //        _context.Article.Update(article);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    return NotFound();
        //}

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var article = _context.Article.FirstOrDefault(a => a.Id == id);
            if (article != null)
            {
                _context.Article.Remove(article);
                _context.SaveChanges();
            }
        }

        [HttpGet("loadMore/{len}")]
        public int LoadMore(int len)
        {
            len += 1;
            var loaded = _context.Article.OrderBy(item => item.Id).Take(len);
            //foreach(var article in loaded)
            //{
            //    article.Category = _context.Category.Where(item => item.Id == article.CategoryId).First();
            //}

            return loaded.Last().Id;
            //if(len >= loaded.ToList().Count)
            //{
            //    return loaded.Last();
            //}
            //return loaded.Take(len).Last();
        }

        [HttpGet("getNext/{inx}")]
        public Article getNext(int inx)
        {
            inx += 1;
            var loaded = _context.Article.OrderBy(item => item.Id).Take(inx);
            //foreach(var article in loaded)
            //{
            //    article.Category = _context.Category.Where(item => item.Id == article.CategoryId).First();
            //}
            if(loaded.ToList().Count < inx)
            {
                return null;
            }

            return loaded.Last();
            //if(len >= loaded.ToList().Count)
            //{
            //    return loaded.Last();
            //}
            //return loaded.Take(len).Last();
        }

        [HttpGet("getArticle/{id}")]
        public Article getArticle(int id)
        {
            var articles = _context.Article.Where(item => item.Id == id);
            if(articles.ToList().Count == 0)
            {
                return null;
            }
            return articles.First();
        }

        //[HttpGet("prev/{id}")]
        //public Article GetPrev(int id) => _context.Article
        //    .Where(item => item.Id < id)
        //    .OrderByDescending(s => s.Id)
        //    .FirstOrDefault();

        //[HttpGet("next/{id}")]
        //public Article GetNext(int id) => _context.Article
        //    .Where(item => item.Id < id)
        //    .OrderBy(s => s.Id)
        //    .FirstOrDefault();
    }
}
