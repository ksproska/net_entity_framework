using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace L10_2.ViewModels
{
    [NotMapped]
    public class OrderDetails
    {
        [Required]
        public int Id{ get; set; }
        [NotMapped]
        public List<CartArticle> CartArticles { get; set; }
        //[Required]
        //[NotMapped]
        //public List<int> ArticleIdWithRepetition { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "To short name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {1}")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short surname")]
        [MaxLength(20, ErrorMessage = " To long surname, do not exceed {1}")]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Write 9 digits")]
        [Display(Name="Phone number")]
        public string PhoneNumber { get;set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short city")]
        [MaxLength(20, ErrorMessage = " To long city, do not exceed {1}")]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Example: 00-000")]
        public string Postcode { get; set; }
        
        [NotMapped]
        [Display(Name = "Type")]
        public PaymentOption PaymentOption { get; set; }
        [Required]
        public int PaymentOptionId { get; set; }

        public OrderDetails() { }
    }
}
