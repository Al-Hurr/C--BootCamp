using d07_ex03.Models;
using System;

namespace d07_ex03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(typeof(IdentityUser));
            IdentityUser user1 = TypeFactory.CreateWithConstructor<IdentityUser>();
            IdentityUser user2 = TypeFactory.CreateWithActivator<IdentityUser>();
            Console.WriteLine(user1 == user2 ? "user1 == user2" : "user1 != user2");

            Console.WriteLine();

            Console.WriteLine(typeof(IdentityRole));
            IdentityRole role1 = TypeFactory.CreateWithConstructor<IdentityRole>();
            IdentityRole role2 = TypeFactory.CreateWithActivator<IdentityRole>();
            Console.WriteLine(role1 == role2 ? "role1 == role2" : "role1 != role2");

            Console.WriteLine(typeof(IdentityUser));
            Console.WriteLine("Set name:");
            string name = Console.ReadLine();
            IdentityUser user3 = TypeFactory.CreateWithParameters<IdentityUser>(new object[]{ name});
            Console.WriteLine($"Username set: {user3.UserName}");

            Console.ReadLine();
        }
    }
}
