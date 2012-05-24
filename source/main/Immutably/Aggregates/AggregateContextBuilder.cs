using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    public class AggregateContextBuilder<TState>
    {
        private String _aggregateId = "temporary_id";
        private TState _aggregateState;
        private IDataFactory _dataFactory;
        private Int32 _aggregateVersion = 0;

        public AggregateContextBuilder<TState> SetId(String id)
        {
            _aggregateId = id;
            return this;
        }

        public AggregateContextBuilder<TState> SetVersion(Int32 version)
        {
            _aggregateVersion = version;
            return this;
        }

        public AggregateContextBuilder<TState> SetState(TState state)
        {
            _aggregateState = state;
            return this;
        }

        public AggregateContextBuilder<TState> SetDataFactory(IDataFactory dataFactory)
        {
            _dataFactory = dataFactory;
            return this;
        }

        public AggregateContext Build()
        {
            if (_aggregateState == null && _dataFactory == null)
                _aggregateState = Activator.CreateInstance<TState>();

            return new StatefullAggregateContext(_aggregateState, _aggregateId, _aggregateVersion, _dataFactory);
        }
    }
}