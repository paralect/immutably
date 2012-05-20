using System;

namespace Immutably.Data
{
    public interface IDataFactory
    {
        TDataType Create<TDataType>();
        Object Create(Type type);
    }
}