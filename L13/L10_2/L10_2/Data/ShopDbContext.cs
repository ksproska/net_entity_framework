using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L10_2.Models;
using L10_2.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace L10_2.Data
{
    public class ShopDbContext: IdentityDbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article { get; set; }
        //public DbSet<L10_2.ViewModels.CartArticle> CartArticle { get; set; }
        public DbSet<L10_2.ViewModels.PaymentOption> PaymentOption { get; set; }
        //public DbSet<L10_2.ViewModels.OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }
    }
}
