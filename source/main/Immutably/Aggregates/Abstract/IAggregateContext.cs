using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public interface IAggregateContext
    {
        Object State { get; }
        String Id { get; }
        Int32 CurrentVersion { get; }
        IDataFactory DataFactory { get; }
        Boolean Changed { get; }
        IList<IEvent> Changes { get; }
        void Apply(IEvent evnt);
        void Reply(IEvent evnt);
        void Reply(IEnumerable<IEvent> events);
    }
}