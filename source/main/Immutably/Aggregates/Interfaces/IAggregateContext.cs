using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Utilities;

namespace Immutably.Aggregates
{
    public interface IAggregateContext
    {
        String Id { get; }

        Int32 CurrentVersion { get; }

        Int32 InitialVersion { get; }

        IDataFactory DataFactory { get; }

        Boolean Changed { get; }

        IIndexedEnumerable<Object> Changes { get; }

        void Apply(Object evnt);

        void Apply<TEvent>(Action<TEvent> evntBuilder);

        TData Create<TData>();
    }

    public interface IStatefullAggregateContext : IAggregateContext
    {
        Object State { get; }

        void Replay(Object evnt);

        void Replay(IEnumerable<Object> events);
    }

    public interface IStatelessAggregateContext : IAggregateContext
    {
        
    }
}