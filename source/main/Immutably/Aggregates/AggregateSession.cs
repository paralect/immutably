using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Immutably.Data;
using Immutably.States;
using Immutably.Transitions;

namespace Immutably.Aggregates
{
    public class AggregateSession : IAggregateSession
    {
        /// <summary>
        /// Aggregate store, this session is working with
        /// </summary>
        private readonly AggregateStore _store;

        /// <summary>
        /// DataFactory, used to create data types, such as state and messages (events, commands)
        /// </summary>
        private readonly IDataFactory _dataFactory;


        private readonly AggregateFactory _aggregateFactory;

        /// <summary>
        /// Aggregates, opened or created in this session. (i.e. Unit of Work)
        /// </summary>
        private readonly List<IAggregate> _aggregates = new List<IAggregate>();

        /// <summary>
        /// Creates AggregateSession
        /// </summary>
        public AggregateSession(AggregateStore store, IDataFactory dataFactory, AggregateFactory aggregateFactory)
        {
            _store = store;
            _dataFactory = dataFactory;
            _aggregateFactory = aggregateFactory;
        }

        /// <summary>
        /// Returns aggregate or throws exception if aggregate wasn't found
        /// </summary>
        public IAggregate LoadAggregate(Type aggregateType, String aggregateId)
        {
            var aggregate = LoadAggregateInternal(aggregateType, aggregateId);

            if (aggregate == null)
                throw new AggregateDoesntExistException(aggregateType, aggregateId);

            return aggregate;
        }

        /// <summary>
        /// Returns aggregate or null, if it wasn't find
        /// </summary>
        private IAggregate LoadAggregateInternal(Type aggregateType, String aggregateId)
        {
            // We don't allow null id 
            if (aggregateId == null)
                throw new NullAggregateIdException();

            var definition = _aggregateFactory.GetAggregateDefinition(aggregateType);

            // Check, if this is stateless aggregate
            if (definition.AggregateKind == AggregateKind.Stateless)
            {
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

            // Check, if this is statefull aggregate
            if (definition.AggregateKind == AggregateKind.Statefull)
            {
                // Here we can load state from snapshot store, but we are starting from initial state.
                var initialState = _store.CreateState(definition.StateType);

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

            throw new Exception("Specified AggregateKind is not supported");
        }

        /// <summary>
        /// Returns aggregate if it already exists, or creates new.
        /// </summary>
        public IAggregate LoadOrCreateAggregate(Type aggregateType, String aggregateId)
        {
            var aggregate = LoadAggregateInternal(aggregateType, aggregateId);

            if (aggregate == null)
                aggregate = CreateAggregate(aggregateType, aggregateId);

            return aggregate;
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public IAggregate CreateAggregate(Type aggregateType, String aggregateId)
        {
            var definition = _aggregateFactory.GetAggregateDefinition(aggregateType);

            if (definition.AggregateKind == AggregateKind.Statefull)
            {
                var initialState = _store.CreateState(definition.StateType);
                return EstablishStatefullAggregate(aggregateType, initialState, aggregateId, 0, _dataFactory);
            }

            if (definition.AggregateKind == AggregateKind.Stateless)
            {
                return EstablishStatelessAggregate(aggregateType, aggregateId, 0, _dataFactory);
            }

            throw new Exception(String.Format("Cannot create aggregate of type {0}", aggregateType));
        }

        private IStatefullAggregate EstablishStatefullAggregate(Type aggregateType, Object state, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatefullAggregate(aggregateType);
            var context = new StatefullAggregateContext(state, aggregateId, version, dataFactory);
            aggregate.EstablishContext(context);

            _aggregates.Add(aggregate);

            return aggregate;
        }

        private IStatelessAggregate EstablishStatelessAggregate(Type aggregateType, String aggregateId, Int32 version, IDataFactory dataFactory)
        {
            var aggregate = _store.CreateStatelessAggregate(aggregateType);
            var context = new StatelessAggregateContext(aggregateId, version, dataFactory);
            aggregate.EstablishContext(context);

            _aggregates.Add(aggregate);

            return aggregate;
        }

        public void SaveChanges()
        {
            if (_aggregates.Count == 0)
                return;

            foreach (var aggregate in _aggregates)
            {
                // Skip aggregates without changes
                if (aggregate.Changes.Count <= 0)
                    continue;

                using (var writer = _store.TransitionStore.CreateStreamWriter(aggregate.Id))
                {
                    writer.Write(aggregate.CurrentVersion, builder => builder
                        .AddEvents(aggregate.Changes)
                    );
                }
            }

            // Clear unit of work container
            _aggregates.Clear();
        }

        public TAggregate CreateAggregate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate
        {
            return(TAggregate) CreateAggregate(typeof(TAggregate), aggregateId);
        }

        public TAggregate LoadAggregate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate
        {
            return (TAggregate) LoadAggregate(typeof(TAggregate), aggregateId);
        }

        public TAggregate LoadOrCreateAggregate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate
        {
            return (TAggregate) LoadOrCreateAggregate(typeof (TAggregate), aggregateId);
        }

        public void Dispose()
        {

        }
    }
}