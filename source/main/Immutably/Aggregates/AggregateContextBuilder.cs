using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    public class AggregateContextBuilder
    {
        protected String _aggregateId = "temporary_id";
        protected Object _aggregateState;
        protected IDataFactory _dataFactory;
        protected Int32 _aggregateVersion = 0;

        public StatelessAggregateContext Build()
        {
            return new StatelessAggregateContext(_aggregateId, _aggregateVersion, _dataFactory);
        }
    }

    public class AggregateContextBuilder<TState> : AggregateContextBuilder
    {
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

        public new StatefullAggregateContext Build()
        {
            if (_aggregateState == null && _dataFactory == null)
                _aggregateState = Activator.CreateInstance<TState>();

            return new StatefullAggregateContext(_aggregateState, _aggregateId, _aggregateVersion, _dataFactory);
        }

    }
}