using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMyCurrency.Models
{
    public class MVCAlert
    {

        [Key]
        public int AlertId { get; set; }

        [Required(ErrorMessage ="The email is required")]
        [MaxLength(ErrorMessage ="The max length is 150 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The currency name is required")]
        [MaxLength(3,ErrorMessage = "The max length is 3 characters")]
        [MinLength(3,ErrorMessage = "The min length is 3 characters")]
        public string CurrencyName { get; set; }
        [Range(minimum:0, maximum: 9999.99999,ErrorMessage ="Invalid value.")]
        public double BestValue { get; set; }

    }
}
