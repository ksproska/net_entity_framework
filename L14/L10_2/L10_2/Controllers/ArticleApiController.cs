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

        //[HttpGet("prev/{id}")]
        //public Student GetPrev(int id) => repository.GetPreviousStudent(id);

        //[HttpGet("next/{id}")]
        //public Student GetNext(int id) => repository.GetNextStudent(id);
    }
}
