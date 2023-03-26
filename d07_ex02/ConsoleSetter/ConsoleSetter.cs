using d07_ex02.Attributes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace d07_ex02.ConsoleSetterSpace
{
    public class ConsoleSetter
    {
        public void SetValues<T>(T input) where T : class
        {
            Type type= typeof(T);
            Console.WriteLine($"Let's set {type.Name}!");

            var propInfo = type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.CustomAttributes.All(x => !x.AttributeType.Equals(typeof(NoDisplayAttribute))))
                .ToArray();

            foreach (var proerty in propInfo)
            {
                Console.WriteLine($"Set {proerty.Name}:");
                string value = Console.ReadLine();
                if (string.IsNullOrEmpty(value))
                {
                    value = proerty.CustomAttributes
                        .FirstOrDefault(x => x.AttributeType
                        .Equals(typeof(DefaultValueAttribute))).ConstructorArguments[0]
                        .ToString();
                }
                proerty.SetValue(input, value);
            }
            Console.WriteLine();
            Console.WriteLine("We've set our instance!");
            Console.WriteLine(input.ToString());
        }
    }
}