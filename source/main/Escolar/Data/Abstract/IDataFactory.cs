using System;

namespace Escolar.Data
{
    public interface IDataFactory
    {
        TDataType Create<TDataType>();
        Object Create(Type type);
    }
}