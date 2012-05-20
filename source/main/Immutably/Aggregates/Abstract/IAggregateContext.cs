using Immutably.Messages;

namespace Immutably.Aggregates
{
    public interface IAggregateContext<TAggregateId>
    {
        IState State { get; }
        IEscolarFactory Factory { get; }
        void Apply(IEvent evnt);
    }
}