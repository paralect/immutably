using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public interface IAggregateContext
    {
        Object State { get; }
        Object Id { get; }
        int CurrentVersion { get; }
        IDataFactory DataFactory { get; }
        Boolean Changed { get; }
        IList<IEvent> Changes { get; }
        void Apply(IEvent evnt);
        void Reply(IEvent evnt);
        void Reply(IEnumerable<IEvent> events);
    }
}