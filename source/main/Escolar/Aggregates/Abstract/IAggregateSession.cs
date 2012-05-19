using System;
using Escolar.Aggregates;

namespace Escolar.Aggregates
{
    public interface IAggregateSession<TId> : IDisposable
    {
        TAggregateType LoadAggregate<TAggregateType>();
        TAggregateType CreateAggregate<TAggregateType>();
        TAggregateType LoadOrCreateAggregate<TAggregateType>();

        void SaveChanges();
    }
}