using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using L12.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace L12.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }

        //public virtual EntityEntry<Category> Remove([NotNullAttribute] Category entity)
        //{
        //    var article = Article.FirstOrDefaultAsync(a => a.CategoryId == entity.Id);
        //    Console.WriteLine(article);
        //    if (article != null)
        //    {
        //        return null;
        //    }
        //    return base.Remove(entity);
        //}
    }
}
