using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Day_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var timePerItemStr = config["CashRegister:TimePerItem"];
            var timePerCustomerStr = config["CashRegister:TimePerCustomer"];

            if (!Int32.TryParse(timePerItemStr, out int timePerItem)
                || !Int32.TryParse(timePerCustomerStr, out int timePerCustomer))
            {
                Console.WriteLine("parse error");
                Console.ReadLine();

                return;
            }

            Random rnd = new Random();
            Store store = new(storageCapacity: 50, cashRegistersNumber: 4, TimeSpan.FromSeconds(timePerItem), TimeSpan.FromSeconds(timePerCustomer));
            int counter = 1;
            store.CashRegisters.ForEach(x => FillCashRegisterByCustomerRandomly(x, rnd, 4, ref counter));
            List<Customer> customers = new()
            {
                new Customer("Andrew", counter++, rnd),
                new Customer("Mike", counter++, rnd),
                new Customer("Tim", counter++, rnd),
                new Customer("James", counter++, rnd),
                new Customer("Robert", counter++, rnd),
                new Customer("John", counter++, rnd),
                new Customer("Michael", counter++, rnd),
                new Customer("David", counter++, rnd),
                new Customer("William", counter++, rnd),
                new Customer("Richard", counter++, rnd),
                new Customer("aa", counter++, rnd),
                new Customer("ss", counter++, rnd),
                new Customer("dd", counter++, rnd),
                new Customer("ff", counter++, rnd),
                new Customer("gg", counter++, rnd),
                new Customer("hh", counter++, rnd),
                new Customer("jj", counter++, rnd),
                new Customer("kk", counter++, rnd),
                new Customer("ll", counter++, rnd),
                new Customer("nn", counter++, rnd),
            };

            // 1) новые покупатели появляются каждые 7 секунд
            new Thread(() => 
            {
                foreach (var customer in customers)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(7));

                    customer.FillCart(7);

                    var cashRegister = customer.GetCashRegisterWithLeastCustomersNumber(store.CashRegisters);

                    Console.WriteLine($"{customer} ({customer.CartItemsCount} items in cart)" +
                            $" {cashRegister} ({cashRegister.Customers.Count} people with {cashRegister.Customers.Sum(x => x.CartItemsCount)} items behind)");
                }
            }).Start();

            //Parallel.ForEachAsync(customers, customer =>
            //{
            //    customer.FillCart(7);

            //    var cashRegister = customer.GetCashRegisterWithLeastGoodsAmongCustomers(store.CashRegisters);
            //    //cashRegister.Customers.Enqueue(customer);

            //    Console.WriteLine($"{customer} ({customer.CartItemsCount} items in cart)" +
            //            $" {cashRegister} ({cashRegister.Customers.Count} people with {cashRegister.Customers.Sum(x => x.CartItemsCount)} items behind)");
            //});

            // 2) обработка покупателей на кассах

            store.OpenRegisters();

            if(store.CashRegisters.Any(x => x.Customers.Count > 1))

            Console.WriteLine("End");
            Console.ReadLine();

            Console.ReadLine();
        }

        private static void FillCashRegisterByCustomerRandomly(CashRegister cashRegister, Random rnd, int maxCustomersInCash, ref int counter)
        {
            var customersInCash = rnd.Next(1, maxCustomersInCash + 1);
            for(int i = 0; i < customersInCash; i++)
            {
                cashRegister.Customers.Enqueue(new Customer($"No name customer {counter}", counter, rnd));
                counter++;
            }
        }

        //private static void FillCashRegisterByCustomerRandomly(CashRegister cashRegister, Random rnd, int maxCustomersInCash, ref int counter)
        //{
        //    var customersInCash = rnd.Next(1, maxCustomersInCash + 1);
        //    for(int i = 0; i < customersInCash; i++)
        //    {
        //        cashRegister.Customers.Enqueue(new Customer($"No name customer {counter}", counter, rnd));
        //        counter++;
        //    }
        //}
    }
}
