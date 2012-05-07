using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregate
    {
        void Initialize(IStateEnvelope state);
    }
}