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


        private readonly AggregateRegistry _aggregateRegistry;

        /// <summary>
        /// Aggregates, opened or created in this session. (i.e. Unit of Work)
        /// </summary>
        private readonly List<IAggregate> _aggregates = new List<IAggregate>();

        /// <summary>
        /// Creates AggregateSession
        /// </summary>
        public AggregateSession(AggregateStore store, IDataFactory dataFactory, AggregateRegistry aggregateRegistry)
        {
            _store = store;
            _dataFactory = dataFactory;
            _aggregateRegistry = aggregateRegistry;
        }

        /// <summary>
        /// Returns aggregate or throws exception if aggregate wasn't found
        /// </summary>
        public IAggregate LoadAggregate(Type aggregateType, String aggregateId)
        {
            try
            {
                var repository = CreateRepository(aggregateType);
                var aggregate = repository.LoadAggregate(aggregateType, aggregateId);

                RegisterAggregateInSession(aggregate);
                return aggregate;
            }
            catch(TransitionException e)
            {
                throw new AggregateDoesntExistException(aggregateType, aggregateId);
            }
        }

        /// <summary>
        /// Returns aggregate if it already exists, or creates new.
        /// </summary>
        public IAggregate LoadOrCreateAggregate(Type aggregateType, String aggregateId)
        {
            IAggregate aggregate = null;
            var repository = CreateRepository(aggregateType);

            try
            {
                aggregate = repository.LoadAggregate(aggregateType, aggregateId);
            }
            catch(TransitionStreamNotExistsException e)
            {
                aggregate = repository.CreateAggregate(aggregateType, aggregateId);
            }

            RegisterAggregateInSession(aggregate);
            return aggregate;
        }

        /// <summary>
        /// Creates and returns aggregate of specified type
        /// </summary>
        public IAggregate CreateAggregate(Type aggregateType, String aggregateId)
        {
            var repository = CreateRepository(aggregateType);
            var aggregate = repository.CreateAggregate(aggregateType, aggregateId);

            RegisterAggregateInSession(aggregate);
            return aggregate;
        }

        public void SaveChanges()
        {
            if (_aggregates.Count == 0)
                return;

            foreach (var aggregate in _aggregates)
            {
                // Skip aggregates without changes. Nothing to save here.
                if (aggregate.Changes.Count <= 0)
                    continue;

                using (var writer = _store.TransitionStore.CreateStreamWriter(aggregate.Id))
                {
                    writer.Write(aggregate.CurrentVersion, builder => builder
                        .AddEvents(aggregate.Changes)
                    );
                }
            }

            // Clear "unit of work" container
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

        /// <summary>
        /// Creates registry for aggregate based on AggregateKind.
        /// </summary>
        private AggregateRepositoryBase CreateRepository(Type aggregateType)
        {
            var definition = _aggregateRegistry.GetAggregateDefinition(aggregateType);

            if (definition.AggregateKind == AggregateKind.Statefull)
                return new StatefullAggregateRepository(_store, _dataFactory, definition);

            if (definition.AggregateKind == AggregateKind.Stateless)
                return new StatelessAggregateRepository(_store, _dataFactory, definition);

            throw new Exception("Specified AggregateKind is not supported");
        }

        private void RegisterAggregateInSession(IAggregate aggregate)
        {
            _aggregates.Add(aggregate);
        }

        public void Dispose()
        {

        }
    }
}