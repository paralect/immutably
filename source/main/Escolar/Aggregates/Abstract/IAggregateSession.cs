using System;
using Escolar.Aggregates;

namespace Escolar.Aggregates
{
    public interface IAggregateSession<TId, TAggregate> : IDisposable
        where TAggregate : IAggregate<TId>
    {
        TAggregate LoadAggregate();
        TAggregate CreateAggregate();
        TAggregate LoadOrCreateAggregate();

        void SaveChanges();
    }
}