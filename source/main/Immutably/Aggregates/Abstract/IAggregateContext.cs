using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregateContext<TAggregateId>
    {
        IState State { get; }
        IEscolarFactory Factory { get; }
        void Apply(IEvent evnt);
    }
}