using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace L10_2.ViewModels
{
    public class Article
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

        public string ImageFilename { get; set; }

        [NotMapped]
        public string PhotoRelativePath
        {
            get { return !ImageFilename.Equals("") ? Path.Combine("\\" + UPLOAD, ImageFilename) : DefaultImage; }
        }

        public Article() { }
        public Article(int id, string name, double price, Category category, string imageFilename)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Category = category;
            this.ImageFilename = imageFilename;
        }

        public Article(string name, double price, Category category, string imageFilename)
            : this(-1, name, price, category, imageFilename) { }

        public Article(int id, string name, double price, Category category) 
            : this(id, name, price, category, "") { }

        public Article(string name, double price, Category category)
            : this(-1, name, price, category, "") { }
    }
}
