using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIMyCurrency.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string CurrencyTimeStamp { get; set; }
        public string CurrencyName { get; set; }
        public double Value { get; set; }
        public string Base { get; set; }
        public decimal BaseValue { get; set; }

    }
}
