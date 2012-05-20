using System;
using System.Collections.Generic;
using System.Reflection;

namespace Immutably.Data
{
    public class DataContextBuilder
    {
        private readonly Dictionary<Type, DataContract> _contracts = new Dictionary<Type, DataContract>();
        private readonly Dictionary<Type, DataProxy> _proxies = new Dictionary<Type, DataProxy>();
      
        public DataContextBuilder AddContract(Type contractType)
        {
            var contractAttribute = ReflectionHelper.GetAttribute<DataContract>(contractType);
            _contracts.Add(contractType, contractAttribute);
            return this;
        }

        public DataContextBuilder AddContract<TContract>()
        {
            return AddContract(typeof (TContract));
        }

        public DataContextBuilder AddProxy(Type proxyType)
        {
            var proxyAttribute = ReflectionHelper.GetAttribute<DataProxy>(proxyType);
            _proxies.Add(proxyType, proxyAttribute);
            return this;
        }

        public DataContextBuilder AddProxy<TProxy>()
        {
            return AddProxy(typeof (TProxy));
        }

        public DataContextBuilder AddAssemblyContracts(Assembly assembly)
        {
            return AddAssemblyContracts(assembly, null);
        }

        public DataContextBuilder AddAssemblyContracts(Assembly assembly, String namespaceStartsFrom)
        {
            var tuples = ReflectionHelper.GetTypesWithAttributes<DataContract>(assembly, namespaceStartsFrom);

            foreach (var tuple in tuples)
                _contracts.Add(tuple.Item1, tuple.Item2);
            
            return this;
        }


        public DataContextBuilder AddAssemblyProxies(Assembly assembly)
        {
            return AddAssemblyProxies(assembly, null);
        }

        public DataContextBuilder AddAssemblyProxies(Assembly assembly, String namespaceStartsFrom)
        {
            var tuples = ReflectionHelper.GetTypesWithAttributes<DataProxy>(assembly, namespaceStartsFrom);

            foreach (var tuple in tuples)
                _proxies.Add(tuple.Item1, tuple.Item2);

            return this;
        }

        public DataContext Build()
        {
            return new DataContext(_contracts, _proxies);
        }
    }
}