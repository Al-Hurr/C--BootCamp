using System;

namespace Day_01
{
    record Customer
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public int CartItemsCount { get; private set; }

        private Random _rnd;

        public Customer(string name, int number, Random random)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            _rnd = random ?? new Random();
            Name = name;
            Number = number;
        }

        public void FillCart(int cartCapacity) => CartItemsCount = cartCapacity > 1 ? _rnd.Next(1, cartCapacity) : _rnd.Next(1, 10);

        public void TakeItemsFromCart(int itemsCount) => CartItemsCount -= itemsCount;

        //public static bool operator ==(Customer customer1, Customer customer2)
        //{
        //    return customer1.Name == customer2.Name
        //        && customer1.Number == customer2.Number;
        //}

        //public static bool operator !=(Customer customer1, Customer customer2)
        //{
        //    return customer1.Name != customer2.Name
        //        || customer1.Number != customer2.Number;
        //}

        public override string ToString()
        {
            return $"{Name}, customer #{Number}";
        }
    }
}
