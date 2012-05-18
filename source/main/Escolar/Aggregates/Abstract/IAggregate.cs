using System;
using Escolar.Data;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregate
    {
        IState State { get; set; }
        Guid Id { get; set; }
        Int32 CurrentVersion { get; }
        IDataFactory DataFactory { get; set; }

        void Initialize(IAggregateContext factory);

        void Apply(IEvent evnt);
    }
}