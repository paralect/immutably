using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Escolar.Messages;

namespace Escolar.Data
{
    public class ReflectionHelper
    {
        public static IEnumerable<Tuple<Type, TAttribute>> GetTypesWithAttributes<TAttribute>(Assembly assembly, String namespaceStartsFrom = null)
        {
            foreach (Type type in assembly.GetTypes())
            {
                var customAttributes = type.GetCustomAttributes(typeof (TAttribute), true);

                if (customAttributes.Length == 0)
                    continue;

                if (namespaceStartsFrom == null
                    || type.FullName.StartsWith(namespaceStartsFrom))
                    yield return new Tuple<Type, TAttribute>(type, (TAttribute) customAttributes[0]);
            }
        }

        public static TAttribute GetAttribute<TAttribute>(Type type)
        {
            var customAttributes = type.GetCustomAttributes(typeof(TAttribute), true);

            if (customAttributes.Length == 0)
                throw new Exception(String.Format("No [{0}] attribute for type {1}", typeof(TAttribute).FullName, type.FullName));

            return (TAttribute) customAttributes[0];
        }
    }
}