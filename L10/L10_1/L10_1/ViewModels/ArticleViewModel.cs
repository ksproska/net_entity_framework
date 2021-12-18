using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L10_1.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "To short article name")]
        [Display(Name = "Article")]
        [MaxLength(20, ErrorMessage = " To long article name, do not exceed {1}")]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public double Price { get; set; }
        //[Required]
        //public CategoryViewModel Category { get; set; }
        public ArticleViewModel() { }
        public ArticleViewModel(int id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
