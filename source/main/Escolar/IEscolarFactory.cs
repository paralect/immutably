using System;

namespace Escolar
{
    public interface IEscolarFactory
    {
        TType Create<TType>();

        TType Create<TType>(Action<TType> builder);
    }
}