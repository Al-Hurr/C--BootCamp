using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Day_01
{
    record CashRegister
    {
        public string Name { get; private set; }
        public TimeSpan TimePerItem { get; private set; }
        public TimeSpan TimePerCustomer { get; private set; }
        private TimeSpan LoadingTime { get; set; } = TimeSpan.FromSeconds(0);

        public ConcurrentQueue<Customer> Customers { get; set; }

        public CashRegister(string name, TimeSpan timePerItem, TimeSpan timePerCustomer)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Customers = new ConcurrentQueue<Customer>();
            Name = name;
            TimePerItem = timePerItem;
            TimePerCustomer = timePerCustomer;
        }

        public void Process(Customer customer)
        {
            var customerProcessTime = TimePerCustomer + TimePerItem * customer.CartItemsCount;
            LoadingTime += customerProcessTime;
            Console.WriteLine($"Start process in {this} for {customer} with {customer.CartItemsCount} items, wait for {customerProcessTime} seconds");
            Thread.Sleep(customerProcessTime);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
