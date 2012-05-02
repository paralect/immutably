using System;
using Escolar.Aggregates;

namespace Escolar.Aggregates
{
    public interface IAggregateSession : IDisposable
    {
        TAggregate Load<TAggregate>(Guid id)
            where TAggregate : IAggregate;

        void SaveChanges();
    }
}