using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Day_01
{
    class Store
    {
        public Storage Storage { get; private set; }
        public List<CashRegister> CashRegisters { get; private set; }

        public Store(int storageCapacity, int cashRegistersNumber, TimeSpan timePerItem, TimeSpan timePerCustomer)
        {
            Storage = new Storage(storageCapacity);

            CashRegisters = new List<CashRegister>(storageCapacity);

            for (int i = 0; i < cashRegistersNumber; i++)
            {
                CashRegisters.Add(new CashRegister($"Register #{i + 1}", timePerItem, timePerCustomer));
            }
        }

        public void OpenRegisters()
        {
            CashRegisters.Select(cashRegister =>
            {
                var therad = new Thread(() => HandleCustomer(cashRegister));
                therad.Start();
                return therad;
            })
                .ToList()
                .ForEach(x => 
                { 
                    x.Join(); 
                    Console.WriteLine($"Storage goods count {Storage.GoodsCount}"); 
                });
        }

        private void HandleCustomer(CashRegister cashRegister)
        {
            if(cashRegister == null)
            {
                Console.WriteLine("Error. cashRegister is null");
                return;
            }

            while (this.IsOpen())
            {
                if (cashRegister.Customers.Count == 0)
                {
                    Console.WriteLine($"End customers at {cashRegister} Storage.GoodsCount {Storage.GoodsCount}");
                    return;
                }

                if(!cashRegister.Customers.TryDequeue(out Customer customer))
                {
                    Console.WriteLine("TryDequeue customer failed");
                    return;
                }

                cashRegister.Process(customer);

                if (Storage.GoodsCount < customer.CartItemsCount)
                {
                    if (Storage.GoodsCount != 0)
                    {
                        customer.TakeItemsFromCart(Storage.GoodsCount);
                        Storage.ReduceGoods(Storage.GoodsCount);
                    }

                    Console.WriteLine($"{customer} ({customer.CartItemsCount} items left in cart)");
                }
                else
                {
                    Storage.ReduceGoods(customer.CartItemsCount);
                }

                // покупатель появляется каждые 7 сек
                Thread.Sleep(TimeSpan.FromSeconds(7));
            }

            Console.WriteLine($"End goods in storage {Storage.GoodsCount}");
        }

        public bool IsOpen() => !Storage.IsEmpty();
    }
}
