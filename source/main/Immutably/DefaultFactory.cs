using System;

namespace Immutably
{
    public interface IFactory
    {
        Object CreateObject(Type type);
        Object CreateAggregate(Type type);
        Object CreateState(Type type);
    }
}