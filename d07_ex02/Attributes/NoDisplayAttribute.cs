﻿
using System;

namespace d07_ex02.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal class NoDisplayAttribute : Attribute
    {

    }
}
