using System;
using Immutably.Data;
using Immutably.Serialization.Abstract;
using Immutably.States;
using Lokad.Cqrs.TapeStorage;

namespace Immutably.Aggregates
{
    public class StatefullAggregateRepository : AggregateRepositoryBase
    {
        private readonly ISerializer _serializer;
        private readonly AggregateRegistry _aggregateRegistry;
        private readonly ITransitionStore _transitionStore;

        public StatefullAggregateRepository(AggregateStore store, IDataFactory dataFactory, ISerializer serializer, AggregateRegistry aggregateRegistry, ITransitionStore transitionStore, AggregateDefinition definition)
            : base(store, dataFactory, definition)
        {
            _serializer = serializer;
            _aggregateRegistry = aggregateRegistry;
            _transitionStore = transitionStore;
        }

        /// <summary>
        /// Returns aggregate if found or null, if it wasn't found
        /// </summary>
        public override IAggregate LoadAggregate(Type aggregateType, String aggregateId)
        {
            // We don't allow null id 
            if (aggregateId == null)
                throw new NullAggregateIdException();

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(_definition.StateType);

            // Read all transitions and "spool" events in order to build state
            var spooler = new StateSpooler(initialState);

            var definition = _aggregateRegistry.GetAggregateDefinition(aggregateType);
            var container = _transitionStore.GetContainer(definition.AggregateName);
            var stream = container.GetOrCreateStream(aggregateId);
            var transitions = stream.ReadRecords(0, int.MaxValue);

            foreach (var transition in transitions)
            {
                var events = _serializer.Deserialize(transition.Data);
                spooler.Spool(events, transition.Version);
            }
            
            return EstablishStatefullAggregate(aggregateType, spooler.State, aggregateId, (int)spooler.Data, _dataFactory);
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public override IAggregate CreateAggregate(Type aggregateType, String aggregateId)
        {
            var initialState = _store.CreateState(_definition.StateType);
            return EstablishStatefullAggregate(aggregateType, initialState, aggregateId, 0, _dataFactory);
        }

        private IStatefullAggregate EstablishStatefullAggregate(Type aggregateType, Object state, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatefullAggregate(aggregateType);
            var context = new StatefullAggregateContext(state, aggregateId, version, dataFactory);
            aggregate.EstablishContext(context);

            return aggregate;
        }
    }
}