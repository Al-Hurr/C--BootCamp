using System;
using System.Linq;

namespace d02_ex00
{
    class Program
    {
        static void Main(string[] args)
        {
            string sum;
            string ratesDirectory; 

            if(args.Length > 1)
            {
                sum = args[0];
                ratesDirectory = args[1];
            }
            else
            {
                sum = "100 RUB";
                ratesDirectory = @"C:\Users\Азат\Desktop\azat\Development_books\C#\C_sharp_Day02-0\src\rates\rates";
            }

            Exchanger exchanger = new();
            exchanger.FillExchangeRatesListFromDirectory(ratesDirectory);
            var result = exchanger.ConvertToOtherCurrencies(sum);
            Console.WriteLine($"Amount in the original currency: {result.First()}");
            foreach (var item in result.Skip(1))
            {
                Console.WriteLine($"Amount in {item.GetCurrencyTypeStr()}: {item}");
            }
            Console.ReadLine();
        }
    }
    public record Person
    {
        public string Name { get; init; }
        public Person(string name) => Name = name;
    }
}
