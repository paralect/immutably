using System;

namespace Escolar.Data
{
    public interface IDataContext
    {
        Type GetProxy(Type contractType);
        Type GetProxy(Guid contractTag);
        Guid GetTag(Type contractOrProxyType);
    }
}