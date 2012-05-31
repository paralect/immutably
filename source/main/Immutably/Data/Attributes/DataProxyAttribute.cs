using System;

namespace Immutably.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DataProxyAttribute : Attribute
    {
        private readonly Type _dataContractType;

        public DataProxyAttribute()
        {
        }

        public DataProxyAttribute(Type dataContractType)
        {
            _dataContractType = dataContractType;
        }
    }
}