using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L12.Models;
using Microsoft.EntityFrameworkCore;

namespace L12.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }
    }
}
