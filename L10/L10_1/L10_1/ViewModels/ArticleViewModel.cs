using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace L10_1.ViewModels
{
    public class ArticleViewModel
    {
        public static string UPLOAD = "upload";
        public static string IMAGE = "image";
        public static List<CategoryViewModel> AllCategories = new List<CategoryViewModel>() { };
        public List<CategoryViewModel> GetAllCategories()
        {
            return AllCategories;
        }
        public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "To short article name")]
        [Display(Name = "Article name")]
        [MaxLength(20, ErrorMessage = " To long article name, do not exceed {1}")]
        public string Name { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryInx { get; set; }
        
        [Display(Name = "Image")]
        public IFormFile FormFile { get; set; }

        public string PhotoPath
        {
            get { return FormFile != null ? Path.Combine("\\" + UPLOAD, FormFile.FileName) : "/image/no_image.jpg"; }
        }

        public CategoryViewModel getCategory()
        {
            return AllCategories[CategoryInx];
        }

        public ArticleViewModel() { }
        //public ArticleViewModel(int id, string name, double price, CategoryViewModel category, string imgPath)
        //{
        //    this.Id = id;
        //    this.Name = name;
        //    this.Price = price;
        //    this.Category = category;
        //    this.FormFile = imgPath;
        //    if(!AllCategories.Contains(category))
        //    {
        //        AllCategories.Add(category);
        //    }
        //}

        public ArticleViewModel(string name, double price, int categoryInx, IFormFile formFile)
        {
            this.Name = name;
            this.Price = price;
            this.CategoryInx = categoryInx;
            this.FormFile = formFile;
        }
    }
}
