using System;

namespace Immutably.Data
{
    public interface IDataContext
    {
        Type GetProxy(Type contractType);
        Type GetProxy(Guid contractTag);
        Guid GetTag(Type contractOrProxyType);
    }
}