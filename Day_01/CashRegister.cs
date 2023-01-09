using System;
using System.Collections.Generic;

namespace Day_01
{
    record CashRegister
    {
        public string Name { get; private set; }

        public Queue<Customer> Customers { get; set; }

        public CashRegister(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Customers = new Queue<Customer>();
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        //public static bool operator ==(CashRegister cashRegister1, CashRegister cashRegister2)
        //{
        //    return cashRegister1.Name == cashRegister2.Name;
        //}

        //public static bool operator !=(CashRegister cashRegister1, CashRegister cashRegister2)
        //{
        //    return cashRegister1.Name != cashRegister2.Name;
        //}
    }
}
