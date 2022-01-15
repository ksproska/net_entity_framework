using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L13.ViewModels
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short category name")]
        [MaxLength(20, ErrorMessage = " To long category name, do not exceed {1}")]
        public string Name { get; set; }

        public Category() { }

        public Category(string name)
        {
            this.Name = name;
        }
        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
