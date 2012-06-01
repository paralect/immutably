﻿using System;
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
        public IAggregate Load(Type aggregateType, String aggregateId)
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
                throw new AggregateDoesntExistException(aggregateType, aggregateId, e);
            }
        }

        /// <summary>
        /// Returns aggregate if it already exists, or creates new.
        /// </summary>
        public IAggregate LoadOrCreate(Type aggregateType, String aggregateId)
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
        public IAggregate Create(Type aggregateType, String aggregateId)
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
                    writer.Write(aggregate.CurrentVersion, aggregate.Changes);
                }
            }

            // Clear "unit of work" container
            _aggregates.Clear();
        }

        public TAggregate Create<TAggregate>(String aggregateId)
            where TAggregate : IAggregate
        {
            return(TAggregate) Create(typeof(TAggregate), aggregateId);
        }

        public TAggregate Load<TAggregate>(String aggregateId)
            where TAggregate : IAggregate
        {
            return (TAggregate) Load(typeof(TAggregate), aggregateId);
        }

        public TAggregate LoadOrCreate<TAggregate>(String aggregateId)
            where TAggregate : IAggregate
        {
            return (TAggregate) LoadOrCreate(typeof (TAggregate), aggregateId);
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