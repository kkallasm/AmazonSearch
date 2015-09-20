using System;
using System.Collections.Generic;
using System.Web;

namespace AmazonSearch.Models
{
    public class CurrencyModel
    {
        public List<CurrencyConvertor.Currency> currencyCodes { get; set; }
        public CurrencyConvertor.Currency selectedCurrency { get; set; }
        public CurrencyConvertor.Currency baseCurrency { get; set; }
        public double currencyRate { get; set; }
    }
}
