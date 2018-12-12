using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleMyCurrency.Models
{
    class ConsoleAlert
    {
        [Key]
        public int AlertId { get; set; }
        public string Email { get; set; }
        public string CurrencyName { get; set; }
        public double BestValue { get; set; }

    }
}
