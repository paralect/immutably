using System;

namespace Escolar
{
    public interface IAggregateSession
    {
        TAggregate Load<TAggregate>(Guid id)
            where TAggregate : class;

        void SaveChanges();
    }
}