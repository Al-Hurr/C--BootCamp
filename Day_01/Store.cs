
using System.Collections.Generic;

namespace Day_01
{
    class Store
    {
        public Storage Storage { get; private set; }
        public List<CashRegister> CashRegisters { get; private set; }

        public Store(int storageCapacity, int cashRegistersNumber)
        {
            Storage = new Storage(storageCapacity);

            CashRegisters = new List<CashRegister>(storageCapacity);

            for(int i = 0; i < cashRegistersNumber; i++)
            {
                CashRegisters.Add(new CashRegister($"Register #{i + 1}"));
            }
        }

        public bool IsOpen() => !Storage.IsEmpty();
    }
}
