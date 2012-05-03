using Escolar.Aggregates;
using Escolar.Transitions;

namespace Escolar.Aggregates
{
    public class AggregateStore : IAggregateStore
    {
        private readonly IFactory _factory;
        private readonly ITransitionStore _store;

        public AggregateStore(IFactory factory, ITransitionStore store)
        {
            _factory = factory;
            _store = store;
        }

        public IAggregateSession OpenSession()
        {
            return _factory.CreateAggregateSession(_store);
        }
    }
}