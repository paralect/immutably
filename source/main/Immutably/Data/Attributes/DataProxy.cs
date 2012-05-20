using System;

namespace Immutably.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DataProxy : Attribute
    {
        private readonly Type _dataContractType;

        public DataProxy()
        {
        }

        public DataProxy(Type dataContractType)
        {
            _dataContractType = dataContractType;
        }
    }
}