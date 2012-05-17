using System;
using System.Collections.Generic;
using System.Reflection;

namespace Escolar.Data
{
    public class DataContext
    {
        private readonly Dictionary<Guid, DataDefinition> _definitionsByTag = new Dictionary<Guid, DataDefinition>();
        private readonly Dictionary<Type, DataDefinition> _definitionsByType = new Dictionary<Type, DataDefinition>();

        public DataContext(IDictionary<Type, DataContract> contracts, IDictionary<Type, DataProxy> proxies)
        {
            foreach (var pair in contracts)
            {
                var definition = new DataDefinition(pair.Key, Guid.Parse(pair.Value.Tag));

                _definitionsByTag.Add(definition.ContractTag, definition);
                _definitionsByType.Add(definition.ContractType, definition);
            }

            foreach (var pair in proxies)
            {
                var proxyType = pair.Key;

                DataDefinition definition;
                var contractExists = _definitionsByType.TryGetValue(proxyType.BaseType, out definition);

                if (!contractExists)
                    throw new Exception(String.Format("Cannot find contract type for DataProxy {0}", proxyType.FullName));

                definition.ProxyType = proxyType;
            }
        }

        public static DataContext Create(Action<DataContextBuilder> builder)
        {
            var contextBuilder = new DataContextBuilder();
            builder(contextBuilder);
            return contextBuilder.Build();
        }

        public Type GetProxy(Type contractType)
        {
            DataDefinition definition;
            var exists = _definitionsByType.TryGetValue(contractType, out definition);

            if (!exists)
                throw new Exception(String.Format("Contract {0} doesn't registered", contractType.FullName));

            if (definition.ProxyType == null)
                return definition.ContractType;

            return definition.ProxyType;
        }

        public Type GetProxy(Guid contractTag)
        {
            DataDefinition definition;
            var exists = _definitionsByTag.TryGetValue(contractTag, out definition);

            if (!exists)
                throw new Exception(String.Format("Contract tag {0} doesn't registered", contractTag));

            if (definition.ProxyType == null)
                return definition.ContractType;

            return definition.ProxyType;
        }
    }
}