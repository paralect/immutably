using Escolar.Messages;

namespace Escolar.Aggregates
{
    public interface IAggregate
    {
        void Initialize(IAggregateContext factory);

        void Apply(IEvent evnt);
    }
}