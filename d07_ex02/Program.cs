using System;
using d07_ex02.ConsoleSetterSpace;
using d07_ex02.Models;

namespace d07_ex02
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleSetter cs = new ConsoleSetter();
            cs.SetValues(new IdentityUser());

            Console.ReadLine();
        }
    }
}
