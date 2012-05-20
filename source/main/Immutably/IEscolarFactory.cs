using System;

namespace Immutably
{
    public interface IEscolarFactory
    {
        TType Create<TType>();

        TType Create<TType>(Action<TType> builder);
    }
}