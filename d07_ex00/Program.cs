using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace d07_ex00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Type type = typeof(DefaultHttpContext);
            Console.WriteLine($"Type: {type}");
            Console.WriteLine($"Assembly: {type.Assembly}");
            Console.WriteLine($"Based on: {type.BaseType}");
            Console.WriteLine();
            Console.WriteLine("Fields:");
            foreach (var fieldInfo in type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.NonPublic |
                BindingFlags.Public))
            {
                Console.WriteLine($"{fieldInfo.FieldType} {fieldInfo.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Properties:");
            foreach (var propertyInfo in type.GetProperties(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public))
            {
                Console.WriteLine($"{propertyInfo.PropertyType} {propertyInfo.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Methods:");
            foreach (var methodInfo in type.GetMethods())
            {
                Console.WriteLine($"{methodInfo.ReturnType.Name} {methodInfo.Name} ({string.Join(", ", methodInfo.GetParameters().Select(x => $"{x.ParameterType.Name} {x.Name}"))})");
            }

            Console.ReadLine();
        }
    }
}
