
using System.Collections.Generic;
using System.Linq;

namespace Day_01
{
    static class CustomerExtensions
    {
        public static CashRegister GetCashRegisterWithLeastCustomersNumber(this Customer customer, List<CashRegister> cashRegisters)
        {
            var reg = cashRegisters.OrderBy(x => x.Customers.Count).FirstOrDefault();
            reg.Customers.Enqueue(customer);
            return reg;
        }

        public static CashRegister GetCashRegisterWithLeastGoodsAmongCustomers(this Customer customer, List<CashRegister> cashRegisters)
        {
            var reg = cashRegisters.OrderBy(x => x.Customers.Sum(x => x.CartItemsCount)).FirstOrDefault();
            reg.Customers.Enqueue(customer);
            return reg;
        }
    }
}
