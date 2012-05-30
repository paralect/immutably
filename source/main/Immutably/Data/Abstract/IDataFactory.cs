using System;

namespace Immutably.Data
{
    public interface IDataFactory
    {
        TDataType Create<TDataType>();
        TDataType Create<TDataType>(Action<TDataType> builder);
        Object Create(Type type);
    }
}