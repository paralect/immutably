using Escolar.Aggregates;

namespace Escolar.Aggregates
{
    public interface IAggregateStore
    {
        IAggregateSession OpenSession();
    }
}