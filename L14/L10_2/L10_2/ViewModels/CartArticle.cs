using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace L10_2.ViewModels
{
    [NotMapped]
    public class CartArticle
    {
        [NotMapped]
        public static string UPLOAD = "upload";
        [NotMapped]
        public static string DefaultImage = "/image/no_image.jpg";

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short article name")]
        [MaxLength(20, ErrorMessage = " To long article name, do not exceed {1}")]
        public string Name { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        [Display(Name = "Image")]
        public string ImageFilename { get; set; }

        [NotMapped]
        public string PhotoRelativePath
        {
            get { return ImageFilename != null && !ImageFilename.Equals("") ? "/" + UPLOAD + "/" + ImageFilename : DefaultImage; }
        }

        [Required]
        public int Count { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Sum { 
            get { return Count * Price; } 
        }

        public CartArticle() { }

        public CartArticle(Article article, int count)
        {
            this.Id = article.Id;
            this.Name = article.Name;
            this.Price = article.Price;
            this.Category = article.Category;
            this.CategoryId = article.CategoryId;
            this.ImageFilename = article.ImageFilename;
            this.Count = count;
        }

        public CartArticle(Article article, string count)
        {
            this.Id = article.Id;
            this.Name = article.Name;
            this.Price = article.Price;
            this.Category = article.Category;
            this.CategoryId = article.CategoryId;
            this.ImageFilename = article.ImageFilename;
            this.Count = int.Parse(count);
        }
    }
}
