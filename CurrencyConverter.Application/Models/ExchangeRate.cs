using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Application.Models
{
    public class ExchangeRate
    {
        public string Base { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
        public string Date { get; set; }
    }
}
