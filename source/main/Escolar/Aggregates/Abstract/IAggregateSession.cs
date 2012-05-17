using System;
using Escolar.Aggregates;

namespace Escolar.Aggregates
{
    public interface IAggregateSession<TAggregate> : IDisposable
        where TAggregate : IAggregate
    {
        TAggregate LoadAggregate();
        TAggregate CreateAggregate();
        TAggregate LoadOrCreateAggregate();

        void SaveChanges();
    }
}