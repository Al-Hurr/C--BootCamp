
using System.Collections.Generic;
using System.Linq;

namespace Day_01
{
    static class CustomerExtensions
    {
        public static CashRegister GetCashRegisterWithLeastCustomersNumber(this Customer customer, List<CashRegister> cashRegisters)
        {
            return cashRegisters.OrderBy(x => x.Customers.Count).FirstOrDefault();
        }

        public static CashRegister GetCashRegisterWithLeastGoodsAmongCustomers(this Customer customer, List<CashRegister> cashRegisters)
        {
            return cashRegisters.OrderBy(x => x.Customers.Sum(x => x.CartItemsCount)).FirstOrDefault();
        }
    }
}
