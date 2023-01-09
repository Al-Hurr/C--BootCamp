using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            var customer1 = new Customer("Andrew", 1, rnd);
            var customer2 = new Customer("Andrew", 1, rnd);
            Console.WriteLine(customer1 == customer2);
            Store store = new(storageCapacity: 40, cashRegistersNumber: 3);

            List<Customer> customers = new()
            {
                new Customer("Andrew", 1, rnd),
                new Customer("Mike", 2, rnd),
                new Customer("Tim", 3, rnd),
                new Customer("James", 4, rnd),
                new Customer("Robert", 5, rnd),
                new Customer("John", 6, rnd),
                new Customer("Michael", 7, rnd),
                new Customer("David", 8, rnd),
                new Customer("William", 9, rnd),
                new Customer("Richard", 10, rnd)
            };

            while (store.IsOpen())
            {
                foreach (var customer in customers)
                {
                    customer.FillCart(8);
                    // case 1
                    //var cashRegister = customer.GetCashRegisterWithLeastCustomersNumber(store.CashRegisters);

                    // case 2
                    var cashRegister = customer.GetCashRegisterWithLeastGoodsAmongCustomers(store.CashRegisters);
                    cashRegister.Customers.Enqueue(customer);

                    Console.WriteLine($"{customer} ({customer.CartItemsCount} items in cart)" +
                        $" {cashRegister} ({cashRegister.Customers.Count} people with {cashRegister.Customers.Sum(x => x.CartItemsCount)} items behind)");
                }

                foreach (var cashRegister in store.CashRegisters)
                {
                    while (cashRegister.Customers.Count > 0)
                    {
                        var customer = cashRegister.Customers.Dequeue();

                        if (store.Storage.GoodsCount < customer.CartItemsCount)
                        {
                            if (store.Storage.GoodsCount != 0)
                            {
                                customer.TakeItemsFromCart(store.Storage.GoodsCount);
                                store.Storage.GoodsCount = 0;
                            }

                            Console.WriteLine($"{customer} ({customer.CartItemsCount} items left in cart)");
                        }
                        else
                        {
                            store.Storage.GoodsCount -= customer.CartItemsCount;
                        }
                    }
                }
            }

            Console.WriteLine("End");
            Console.ReadLine();

            //var customers1 = new Queue<Customer>();
            //customers1.Enqueue(new Customer("sss", 1));
            //customers1.Enqueue(new Customer("sss", 1));
            //customers1.Enqueue(new Customer("sss", 1));
            //customers1.Enqueue(new Customer("sss", 1));
            //foreach (var customer in customers1)
            //{
            //    customer.FillCart(15);
            //}

            //var customers2 = new Queue<Customer>();
            //customers2.Enqueue(new Customer("sss", 1));
            //customers2.Enqueue(new Customer("sss", 1));
            //foreach (var customer in customers2)
            //{
            //    customer.FillCart(15);
            //}

            //var customers3 = new Queue<Customer>();
            //customers3.Enqueue(new Customer("sss", 1));
            //customers3.Enqueue(new Customer("sss", 1));
            //customers3.Enqueue(new Customer("sss", 1));
            //foreach (var customer in customers3)
            //{
            //    customer.FillCart(15);
            //}

            //List<CashRegister> cr = new List<CashRegister>
            //{
            //    new CashRegister("r1")
            //    {
            //        Customers = customers1
            //    },
            //    new CashRegister("r2")
            //    {
            //        Customers = customers2
            //    },
            //    new CashRegister("r3")
            //    {
            //        Customers = customers3
            //    },
            //};

            //Console.WriteLine(cr.OrderBy(x => x.Customers.Count).FirstOrDefault());
            //Console.WriteLine(cr.OrderBy(x => x.Customers.Sum(x => x.CartItemsCount)).FirstOrDefault());

            Console.ReadLine();
        }
    }
}
