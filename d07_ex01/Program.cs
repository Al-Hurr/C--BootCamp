using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;

namespace d07_ex01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DefaultHttpContext defaultHttpContext= new DefaultHttpContext();
            Console.WriteLine($"Old Response value: {defaultHttpContext.Response}");
            var fieldInfo = typeof(DefaultHttpContext).GetField("_response", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(defaultHttpContext, null);
            Console.WriteLine($"New Response value: {defaultHttpContext.Response}");
            Console.ReadLine();
        }
    }
}
