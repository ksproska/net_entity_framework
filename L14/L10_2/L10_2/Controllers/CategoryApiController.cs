using L10_2.Data;
using L10_2.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace L10_2.Controllers
{
    [EnableCors]
    [Route("api/category")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryApiController : ControllerBase
    {
        private readonly ShopDbContext _context;
        public CategoryApiController(ShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Category> Get() => _context.Category;
        [HttpGet("{id}")]
        public Category Get(int id) => _context.Category.ToList().Where(item => item.Id == id).FirstOrDefault();
        [HttpPost]
        public Category Post([FromBody] Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
            return category;
        }

        [HttpPut]
        public Category Put([FromBody] Category category)
        {
            _context.Category.Update(category);
            _context.SaveChanges();
            return category;
        }

        //[HttpPatch("{id}")]
        //public StatusCodeResult Patch(int id,
        //        [FromBody] JsonPatchDocument<Category> patch)
        //{
        //    //not working
        //    Category category = Get(id);
        //    if (category != null)
        //    {
        //        patch.ApplyTo(category);
        //        _context.Category.Update(category);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    return NotFound();
        //}

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var article = _context.Article.FirstOrDefault(a => a.CategoryId == id);
            var category = _context.Category.FirstOrDefault(m => m.Id == id);
            if (article == null && category != null)
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
        }

        //[HttpGet("prev/{id}")]
        //public Student GetPrev(int id) => repository.GetPreviousStudent(id);

        //[HttpGet("next/{id}")]
        //public Student GetNext(int id) => repository.GetNextStudent(id);
    }
}
