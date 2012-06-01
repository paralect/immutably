using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Utilities;

namespace Immutably.Aggregates
{
    public interface IAggregate
    {
        String Id { get; }

        Int32 CurrentVersion { get; }

        Int32 InitialVersion { get; }

        Boolean Changed { get; }

        IIndexedEnumerable<Object> Changes { get; }

        IDataFactory DataFactory { get; }

        void Apply(Object evnt);
    }

    public interface IStatefullAggregate : IAggregate
    {
        Object State { get; }
        IStatefullAggregateContext Context { get; }

        void Replay(Object evnt);
        void Replay(IEnumerable<Object> events);
        void EstablishContext(IStatefullAggregateContext context);
    }

    public interface IStatelessAggregate : IAggregate
    {
        IStatelessAggregateContext Context { get; }
        void EstablishContext(IStatelessAggregateContext context);
    }
}