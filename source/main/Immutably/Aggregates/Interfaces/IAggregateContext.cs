using System;
using System.Collections.Generic;
using Immutably.Data;
using Immutably.Messages;
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

        IIndexedEnumerable<IEvent> Changes { get; }

        void Apply(IEvent evnt);

        void Apply<TEvent>(Action<TEvent> evntBuilder)
            where TEvent : IEvent;

        TData Create<TData>();
    }

    public interface IStatefullAggregateContext : IAggregateContext
    {
        Object State { get; }

        void Replay(IEvent evnt);
        
        void Replay(IEnumerable<IEvent> events);
    }

    public interface IStatelessAggregateContext : IAggregateContext
    {
        
    }
}