using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    public class AggregateContextBuilder<TId, TState>
    {
        private TId _aggregateId;
        private TState _aggregateState;
        private IDataFactory _dataFactory;
        private Int32 _aggregateVersion;

        public AggregateContextBuilder<TId, TState> SetId(TId id)
        {
            _aggregateId = id;
            return this;
        }

        public AggregateContextBuilder<TId, TState> SetVersion(Int32 version)
        {
            _aggregateVersion = version;
            return this;
        }

        public AggregateContextBuilder<TId, TState> SetState(TState state)
        {
            _aggregateState = state;
            return this;
        }

        public AggregateContextBuilder<TId, TState> SetDataFactory(IDataFactory dataFactory)
        {
            _dataFactory = dataFactory;
            return this;
        }

        public AggregateContext<TId, TState> Build()
        {
            return new AggregateContext<TId, TState>(_aggregateId, _aggregateVersion, _aggregateState, _dataFactory);
        }
    }
}