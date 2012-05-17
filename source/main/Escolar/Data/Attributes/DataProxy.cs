using System;

namespace Escolar.Data
{
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