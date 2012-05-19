using System;
using Escolar.Data;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregate<TId>
    {
        IState State { get; set; }
        TId Id { get; set; }
        Int32 CurrentVersion { get; }
        Int32 InitialVersion { get; set; }
        IDataFactory DataFactory { get; set; }


        void Initialize(IAggregateContext<TId> factory);

        void Apply(IEvent evnt);
    }
}