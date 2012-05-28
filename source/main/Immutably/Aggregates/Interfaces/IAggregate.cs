using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;

namespace Immutably.Aggregates
{
    public interface IAggregate
    {
        String Id { get; }

        Int32 CurrentVersion { get; }

        Int32 InitialVersion { get; }

        IDataFactory DataFactory { get; }

        void Apply(IEvent evnt);
    }

    public interface IStatefullAggregate : IAggregate
    {
        IState State { get; }
        IStatefullAggregateContext Context { get; }

        void Reply(IEvent evnt);
        void Reply(IEnumerable<IEvent> events);
        void EstablishContext(IStatefullAggregateContext context);
    }

    public interface IStatelessAggregate : IAggregate
    {
        IStatelessAggregateContext Context { get; }
        void EstablishContext(IStatelessAggregateContext context);
    }
}