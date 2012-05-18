using System;
using System.Collections.Generic;
using System.Reflection;

namespace Escolar.Data
{
    public class DataContext : IDataContext, IDataFactory
    {
        private readonly Dictionary<Guid, DataDefinition> _definitionsByContractTag = new Dictionary<Guid, DataDefinition>();
        private readonly Dictionary<Type, DataDefinition> _definitionsByContractType = new Dictionary<Type, DataDefinition>();
        private readonly Dictionary<Type, DataDefinition> _definitionsByProxyType = new Dictionary<Type, DataDefinition>();

        public DataContext(IDictionary<Type, DataContract> contracts, IDictionary<Type, DataProxy> proxies)
        {
            foreach (var pair in contracts)
            {
                var definition = new DataDefinition(pair.Key, Guid.Parse(pair.Value.Tag));

                _definitionsByContractTag.Add(definition.ContractTag, definition);
                _definitionsByContractType.Add(definition.ContractType, definition);
            }

            foreach (var pair in proxies)
            {
                var proxyType = pair.Key;

                DataDefinition definition;
                var contractExists = _definitionsByContractType.TryGetValue(proxyType.BaseType, out definition);

                if (!contractExists)
                    throw new Exception(String.Format("Cannot find contract type for DataProxy {0}", proxyType.FullName));

                definition.ProxyType = proxyType;
                _definitionsByProxyType.Add(proxyType, definition);
            }
        }

        public static IDataContext Create(Action<DataContextBuilder> builder)
        {
            var contextBuilder = new DataContextBuilder();
            builder(contextBuilder);
            return contextBuilder.Build();
        }

        public Type GetProxy(Type contractType)
        {
            DataDefinition definition;
            var exists = _definitionsByContractType.TryGetValue(contractType, out definition);

            if (!exists)
                throw new Exception(String.Format("Contract {0} doesn't registered", contractType.FullName));

            if (definition.ProxyType == null)
                return definition.ContractType;

            return definition.ProxyType;
        }

        public Type GetProxy(Guid contractTag)
        {
            DataDefinition definition;
            var exists = _definitionsByContractTag.TryGetValue(contractTag, out definition);

            if (!exists)
                throw new Exception(String.Format("Contract tag {0} doesn't registered", contractTag));

            if (definition.ProxyType == null)
                return definition.ContractType;

            return definition.ProxyType;
        }

        public Guid GetTag(Type contractOrProxyType)
        {
            DataDefinition definitionByContractType;
            _definitionsByContractType.TryGetValue(contractOrProxyType, out definitionByContractType);

            DataDefinition definitionByProxyType;
            _definitionsByProxyType.TryGetValue(contractOrProxyType, out definitionByProxyType);

            if (definitionByContractType != null)
                return definitionByContractType.ContractTag;

            if (definitionByProxyType != null)
                return definitionByProxyType.ContractTag;

            throw new Exception(String.Format("Contract or proxy [{0}] are not registered"));
        }

        public TDataType Create<TDataType>()
        {
            return (TDataType) Create(typeof (TDataType));
        }

        public object Create(Type type)
        {
            var proxyType = GetProxy(type);
            return Activator.CreateInstance(proxyType);
        }
    }
}