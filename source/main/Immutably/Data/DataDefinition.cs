using System;

namespace Immutably.Data
{
    public class DataDefinition
    {
        public Type ContractType { get; set; }
        public Guid ContractTag { get; set; }
        public Type ProxyType { get; set; }

        public DataDefinition(Type contractType, Guid contractTag, Type proxyType)
        {
            ContractType = contractType;
            ContractTag = contractTag;
            ProxyType = proxyType;
        }

        public DataDefinition(Type contractType, Guid contractTag) : this(contractType, contractTag, null)
        {
        }
    }
}