using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregate
    {
        void Initialize(IStateEnvelope stateEnvelope);
        void Apply(IEventEnvelope evnt);
        void Replay(IEnumerable<IEventEnvelope> evnt);
    }
}