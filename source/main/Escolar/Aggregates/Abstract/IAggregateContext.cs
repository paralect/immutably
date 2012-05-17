using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregateContext
    {
        IState State { get; }
        IStateMetadata StateMetadata { get; }
        IEscolarFactory Factory { get; }
        void Apply(IEvent evnt);
    }
}