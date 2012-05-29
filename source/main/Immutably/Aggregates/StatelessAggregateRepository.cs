using System;
using Immutably.Data;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class StatelessAggregateRepository : AggregateRepositoryBase
    {
        public StatelessAggregateRepository(AggregateStore store, IDataFactory dataFactory, AggregateDefinition aggregateDefinition) : base(store, dataFactory, aggregateDefinition)
        {
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public override IAggregate CreateAggregate(Type aggregateType, String aggregateId)
        {
            return EstablishStatelessAggregate(aggregateType, aggregateId, 0, _dataFactory);
        }

        /// <summary>
        /// Returns aggregate or null, if it wasn't find
        /// </summary>
        public override IAggregate LoadAggregate(Type aggregateType, String aggregateId)
        {
            // We don't allow null id 
            if (aggregateId == null)
                throw new NullAggregateIdException();

            ITransition transition;
            using (var reader = _store.TransitionStore.CreateStreamReader(aggregateId))
            {
                // Read last transition
                transition = reader.ReadLast();
            }

            // Aggregate doesn't exists, if transition is null
            if (transition == null)
                return null;

            return EstablishStatelessAggregate(aggregateType, aggregateId, transition.StreamSequence, _dataFactory);
        }

        private IAggregate EstablishStatelessAggregate(Type aggregateType, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatelessAggregate(aggregateType);
            var context = new StatelessAggregateContext(aggregateId, version, dataFactory);
            aggregate.EstablishContext(context);
            return aggregate;
        }
    }
}