using d05.Nasa.Apod;
using Microsoft.Extensions.Configuration;
using System;

namespace d05.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string apiKey = configuration["ApiKey"];

            try
            {
                ApodClient apodClient = new ApodClient(apiKey);

                var results = apodClient.GetAsync(2).Result;

                foreach(var result in results)
                {
                    Console.WriteLine(result);
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }

            Console.ReadLine();
        }
    }
}
