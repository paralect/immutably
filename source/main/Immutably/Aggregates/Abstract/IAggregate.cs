using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public interface IAggregate<TId>
    {
        TId Id { get; }
        IState State { get; }
        Int32 CurrentVersion { get; }
        Int32 InitialVersion { get; }
        IDataFactory DataFactory { get; }

        void Apply(IEvent evnt);
        void Reply(IEvent evnt);
        void Reply(IEnumerable<IEvent> events);
        void EstablishContext(IAggregateContext context);
    }
}