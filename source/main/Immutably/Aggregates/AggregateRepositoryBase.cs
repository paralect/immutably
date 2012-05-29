using System;
using Immutably.Data;
using Immutably.States;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public abstract class AggregateRepositoryBase
    {
        protected readonly AggregateStore _store;
        protected readonly IDataFactory _dataFactory;
        protected readonly AggregateDefinition _definition;

        public AggregateRepositoryBase(AggregateStore store, IDataFactory dataFactory, AggregateDefinition definition)
        {
            _store = store;
            _dataFactory = dataFactory;
            _definition = definition;
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public abstract IAggregate CreateAggregate(Type aggregateType, String aggregateId);

        /// <summary>
        /// Returns aggregate or null, if it wasn't find
        /// </summary>
        public abstract IAggregate LoadAggregate(Type aggregateType, String aggregateId);
    }

    public class StatelessAggregateRepository : AggregateRepositoryBase
    {
        public StatelessAggregateRepository(AggregateStore store, IDataFactory dataFactory, AggregateDefinition aggregateDefinition) : base(store, dataFactory, aggregateDefinition)
        {
        }

        private IAggregate EstablishStatelessAggregate(Type aggregateType, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatelessAggregate(aggregateType);
            var context = new StatelessAggregateContext(aggregateId, version, dataFactory);
            aggregate.EstablishContext(context);
            return aggregate;
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

            // Aggregate doesn't exists, if transition == 0
            if (transition == null)
                return null;

            return EstablishStatelessAggregate(aggregateType, aggregateId, transition.StreamSequence, _dataFactory);
        }
    }

    public class StatefullAggregateRepository : AggregateRepositoryBase
    {
        public StatefullAggregateRepository(AggregateStore store, IDataFactory dataFactory, AggregateDefinition definition) : base(store, dataFactory, definition)
        {
        }

        private IStatefullAggregate EstablishStatefullAggregate(Type aggregateType, Object state, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatefullAggregate(aggregateType);
            var context = new StatefullAggregateContext(state, aggregateId, version, dataFactory);
            aggregate.EstablishContext(context);

            return aggregate;
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public override IAggregate CreateAggregate(Type aggregateType, String aggregateId)
        {
            var initialState = _store.CreateState(_definition.StateType);
            return EstablishStatefullAggregate(aggregateType, initialState, aggregateId, 0, _dataFactory);
        }

        /// <summary>
        /// Returns aggregate or null, if it wasn't find
        /// </summary>
        public override IAggregate LoadAggregate(Type aggregateType, String aggregateId)
        {
            // We don't allow null id 
            if (aggregateId == null)
                throw new NullAggregateIdException();

            // Here we can load state from snapshot store, but we are starting from initial state.
            var initialState = _store.CreateState(_definition.StateType);

            var spooler = new StateSpooler(initialState);
            using (var reader = _store.TransitionStore.CreateStreamReader(aggregateId))
            {
                foreach (var transition in reader.ReadAll())
                    spooler.Spool(transition.Events, transition.StreamSequence);
            }

            // Aggregate doesn't exists, if spooler.Data is null
            if (spooler.Data == null)
                return null;

            return EstablishStatefullAggregate(aggregateType, spooler.State, aggregateId, (int)spooler.Data, _dataFactory);
        }
    }
}