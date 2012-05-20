using System;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public interface IAggregateContext
    {
        Object State { get; }
        Object Id { get; }
        void Apply(IEvent evnt);
    }
}