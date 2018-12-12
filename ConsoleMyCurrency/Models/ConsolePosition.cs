using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleMyCurrency.Models
{
    class ConsolePosition
    {
        [Key]
        public int PositionId { get; set; }
        public string CurrencyTimeStamp { get; set; }
        public string CurrencyName { get; set; }
        public double Value { get; set; }
        public string Base { get; set; }
        public double BaseValue { get; set; }


    }
}
