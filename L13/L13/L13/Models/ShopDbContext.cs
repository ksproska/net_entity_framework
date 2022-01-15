using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L13.Models;
using L13.ViewModels;

namespace L13.Data
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<L13.ViewModels.CartArticle> CartArticle { get; set; }
    }
}
