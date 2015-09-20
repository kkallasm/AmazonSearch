using System;
using System.Collections.Generic;
using System.Web;

namespace AmazonSearch.Models
{
    public class CurrencyService
    {
        public static List<CurrencyConvertor.Currency> GetCurrencyCodes()
        {
            List<CurrencyConvertor.Currency> currencyCodes = new List<CurrencyConvertor.Currency>();

            foreach (CurrencyConvertor.Currency code in Enum.GetValues(typeof(CurrencyConvertor.Currency)))
            {
                currencyCodes.Add(code);
            }

            return currencyCodes;
        }

        public static CurrencyConvertor.Currency ConvertStringToCurrencyConvertor(string currency)
        {
            return (CurrencyConvertor.Currency)Enum.Parse(typeof(CurrencyConvertor.Currency), currency, true);
        }

        public static double GetRate(CurrencyConvertor.Currency fromCurrency, CurrencyConvertor.Currency toCurrency)
        {
            CurrencyConvertor.CurrencyConvertorSoap client = new CurrencyConvertor.CurrencyConvertorSoapClient("CurrencyConvertorSoap");
            double rate = client.ConversionRate(fromCurrency, toCurrency);
            if (rate <= 0.0)
                throw new Exception("Invalid rate");

            return rate;
        }
    }
}
