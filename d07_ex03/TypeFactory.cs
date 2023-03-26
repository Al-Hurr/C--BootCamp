using System;
using System.Text;

namespace d07_ex03
{
    internal class TypeFactory
    {
        public static T CreateWithConstructor<T>() where T : class
        {
            var type = typeof(T);
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if(ctor != null)
            {
                return ctor.Invoke(null) as T;
            }

            return default;
        }

        public static T CreateWithActivator<T>() where T : class
        {
            return Activator.CreateInstance(typeof(T)) as T;
        }

        public static T CreateWithParameters<T>(object[] parameters) where T : class
        {
            return Activator.CreateInstance(typeof(T), parameters) as T;
        }        
    }
}
