using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Escolar.Messages;

namespace Escolar.Data
{
    public class DataScanner
    {
        /// <summary>
        /// Finds in specified assemblies all non-abstract types that implements IMessage
        /// </summary>
        public IEnumerable<Type> ScanAssemblies(params Assembly[] assemblies)
        {
            return GetTypesThatImplements<IMessage>(assemblies).ToList();
        }

        /// <summary>
        /// Finds in all AppDomain assemblies all non-abstract types that implements IIdentity
        /// </summary>
        public IEnumerable<Type> ScanAllAppDomainAssemblies()
        {
            return GetTypesThatImplements<IMessage>(AppDomain.CurrentDomain.GetAssemblies()).ToList();
        }


        #region Reflection helpers

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


        /// <summary>
        /// Finds all non abstract types within specified assemblies that implements TInterface
        /// </summary>
        public static IEnumerable<Type> GetTypesThatImplements<TInterface>(params Assembly[] assemblies)
        {
            var type = typeof(TInterface);
            var types = assemblies.ToList()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && type.IsAbstract == false);

            return types;
        }


        #endregion
    }
}