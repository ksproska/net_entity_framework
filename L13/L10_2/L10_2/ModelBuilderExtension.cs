using L10_2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_2
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentOption>().HasData(
                new PaymentOption()
                {
                    Id = 1,
                    Name = "bank transfer"
                },
                new PaymentOption()
                {
                    Id = 2,
                    Name = "blick"
                },
                new PaymentOption()
                {
                    Id = 3,
                    Name = "cash"
                }

                ); ;

        }
    }
}
