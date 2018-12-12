using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIMyCurrency.Models
{
    public class Alert
    {
        [Key]
        public int AlertId { get; set; }
        public string Email { get; set; }
        public string CurrencyName { get; set; }
        public decimal BestValue { get; set; }

    }
}
