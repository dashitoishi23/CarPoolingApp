using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CarPoolingApp.Helpers
{
    public class AttributeDisplayHelper
    {
        public static void DisplayAttributes<T>(T objectType)
        {
            Type type = objectType.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(objectType));
            }
        }
    }
}
