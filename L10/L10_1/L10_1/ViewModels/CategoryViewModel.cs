using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L10_1.ViewModels
{
    public class CategoryViewModel
    {
        //public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short category name")]
        //[Display(Name = "Category")]
        [MaxLength(20, ErrorMessage = " To long category name, do not exceed {1}")]
        public string Name { get; set; }

        public CategoryViewModel() { }

        public CategoryViewModel(string name)
        {
            this.Name = name;
        }
    }
}
