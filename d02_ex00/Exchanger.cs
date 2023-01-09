using d02_ex00.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace d02_ex00
{
    class Exchanger
    {
        public List<ExchangeRate> ExchangeRates { get; set; }

        public Exchanger()
        {
            ExchangeRates = new List<ExchangeRate>();
        }

        public void FillExchangeRatesListFromDirectory(string path)
        {
            string[] filePaths = Directory.GetFiles(path, "*.txt");
            foreach(string filePath in filePaths)
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                string fileName = Path.GetFileName(filePath);

                CurrencyType currencyIdentityFrom = GetCurrencyTypeFromString(fileName);

                foreach(string line in lines)
                {
                    var lineItems = line.Split(':');

                    if(lineItems.Length < 2) { continue; }

                    CurrencyType currencyIdentityTo = GetCurrencyTypeFromString(lineItems[0]);

                    if(!double.TryParse(lineItems[1].Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out double rate))
                    {
                        continue;
                    }

                    ExchangeRates.Add(new ExchangeRate
                    {
                        CurrencyFrom = currencyIdentityFrom,
                        CurrencyTo = currencyIdentityTo,
                        Rate = rate
                    });
                }
            }
        }

        public List<ExchangeSum> ConvertToOtherCurrencies(string inputAmmount)
        {
            var inputAmmountItems = inputAmmount.Trim().Split(' ');

            if(inputAmmountItems.Length < 2)
            {
                return null;
            }

            if (!double.TryParse(inputAmmountItems[0].Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out double ammount))
            {
                return null;
            }

            CurrencyType inputCurrencyType = GetCurrencyTypeFromString(inputAmmountItems[1]);
            List<ExchangeSum> exchangeSums = new()
            {
                new()
                {
                    Amount = ammount,
                    CurrencyType = inputCurrencyType
                }
            };

            foreach (var exchangeRate in ExchangeRates.Where(x => x.CurrencyFrom == inputCurrencyType))
            {
                double currencyAmmount = exchangeRate.Rate * ammount;
                exchangeSums.Add(new()
                {
                    Amount = currencyAmmount,
                    CurrencyType = exchangeRate.CurrencyTo
                });
            }

            return exchangeSums;
        }

        private CurrencyType GetCurrencyTypeFromString(string fileName)
        {
            return fileName switch
            {
                string when fileName.Contains("EUR") => CurrencyType.EUR,
                string when fileName.Contains("RUB") => CurrencyType.RUB,
                string when fileName.Contains("USD") => CurrencyType.USD
            };
        }
    }
}
